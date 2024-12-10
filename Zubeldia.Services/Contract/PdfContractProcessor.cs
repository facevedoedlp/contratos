using System.Text;
using System.Text.RegularExpressions;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using Microsoft.AspNetCore.Http;
using Zubeldia.Domain.Entities;
using Zubeldia.Domain.Interfaces.Services;

public class PdfContractProcessor : IPdfContractProcessor
{
    public async Task<(Contract contract, List<ContractSalary> salaries)> ProcessContractPdf(IFormFile pdfFile, Contract contract)
    {
        var text = await ExtractTextFromPdf(pdfFile);
        return ParseContractData(text, contract);
    }

    private async Task<string> ExtractTextFromPdf(IFormFile file)
    {
        using var ms = new MemoryStream();
        await file.CopyToAsync(ms);
        ms.Position = 0;

        using var pdfReader = new PdfReader(ms);
        using var pdfDocument = new PdfDocument(pdfReader);
        var text = new StringBuilder();

        for (int i = 1; i <= pdfDocument.GetNumberOfPages(); i++)
        {
            var strategy = new SimpleTextExtractionStrategy();
            var pageText = PdfTextExtractor.GetTextFromPage(pdfDocument.GetPage(i), strategy);
            text.AppendLine(CleanText(pageText));
        }

        return text.ToString();
    }

    private (Contract contract, List<ContractSalary> salaries) ParseContractData(string pdfText, Contract contract)
    {
        ParseContractHeaderData(pdfText, contract);

        var salaries = ParseSalariesData(pdfText);

        var objetives = ParseObjectivesData(pdfText);

        contract.Salaries = salaries;

        return (contract, salaries);
    }

    private void ParseContractHeaderData(string pdfText, Contract contract)
    {
        // Buscar fecha de inicio del contrato
        var startPattern = @"En La Plata \(Argentina\), a (\d{1,2} de \w+ de \d{4})";
        var startMatch = Regex.Match(pdfText, startPattern);
        if (startMatch.Success)
        {
            contract.StartDate = ParseSpanishDate(startMatch.Groups[1].Value);
        }

        // Buscar fecha de fin del contrato (en este caso, es explícita en el texto)
        var endPattern = @"hasta el dia (\d{1,2} de \w+ de \d{4})";
        var endMatch = Regex.Match(pdfText, endPattern);
        if (endMatch.Success)
        {
            contract.EndDate = ParseSpanishDate(endMatch.Groups[1].Value);
        }
    }

    private List<ContractSalary> ParseSalariesData(string pdfText)
    {
        var salaries = new List<ContractSalary>();

        // Patrón actualizado para capturar específicamente el formato numérico entre paréntesis
        var salaryPattern = @"Salarios:\s*desde\s*el\s*(\d{1,2}\s+de\s+\w+\s+de[l]?\s+\d{4})\s*hasta\s*el\s*(\d{1,2}\s+de\s+\w+\s+de[l]?\s+\d{4}).*?\(\$\s*([\d\.,]+),00\)";

        var matches = Regex.Matches(pdfText, salaryPattern, RegexOptions.IgnoreCase | RegexOptions.Singleline);

        foreach (Match match in matches)
        {
            try
            {
                var startDate = ParseSpanishDate(match.Groups[1].Value);
                var endDate = ParseSpanishDate(match.Groups[2].Value);

                // Procesar el monto numérico
                var amountStr = match.Groups[3].Value.Replace(".", "");
                var amount = decimal.Parse(amountStr);

                var salary = new ContractSalary
                {
                    StartDate = startDate,
                    EndDate = endDate,
                    Amount = amount,
                    CurrencyId = 1,
                    ExchangeRate = 1,
                    InstallmentsCount = 12,
                    TotalRecognition = amount * 12,
                    InstallmentRecognition = amount,
                };

                salaries.Add(salary);

                Console.WriteLine($"Encontrado salario: {startDate:dd/MM/yyyy} - {endDate:dd/MM/yyyy}: ${amount:N2}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error procesando salario: {ex.Message}");
                Console.WriteLine($"Texto del match: {match.Value}");
            }
        }

        return salaries;
    }

    private List<ContractObjective> ParseObjectivesData(string pdfText)
    {
        var objectives = new List<ContractObjective>();

        // Patrón que maneja las fechas con "de" pegado o separado
        var objectivePattern = @"Del\s*(\d{1,2})(?:de|\s+de)\s*(\w+)(?:\s+de|\s+del)\s*(\d{4})\s*al\s*(\d{1,2})(?:de|\s+de)\s*(\w+)(?:\s+de|\s+del)\s*(\d{4}):\s*o\s*La\s*suma\s*de\s*(?:un |dos |tres |cuatro )?(?:millon(?:es)? )?[^(]*\((\d[\d\.,]+),00\s*\$\)";

        var matches = Regex.Matches(pdfText, objectivePattern, RegexOptions.IgnoreCase | RegexOptions.Singleline);

        foreach (Match match in matches)
        {
            try
            {
                // Construir las fechas
                var startDateStr = $"{match.Groups[1].Value} de {match.Groups[2].Value} de {match.Groups[3].Value}";
                var endDateStr = $"{match.Groups[4].Value} de {match.Groups[5].Value} de {match.Groups[6].Value}";

                var startDate = ParseSpanishDate(startDateStr);
                var endDate = ParseSpanishDate(endDateStr);

                var amountStr = match.Groups[7].Value.Replace(".", "");
                var amount = decimal.Parse(amountStr);

                // Extraer el texto para la descripción
                var startIndex = match.Index + match.Length;
                var nextDelIndex = pdfText.IndexOf("Del", startIndex, StringComparison.OrdinalIgnoreCase);
                if (nextDelIndex == -1) nextDelIndex = pdfText.Length;
                var textBlock = pdfText.Substring(startIndex, nextDelIndex - startIndex);

                var isRepeatable = textBlock.Contains("cada vez", StringComparison.OrdinalIgnoreCase);

                var descriptionMatch = Regex.Match(textBlock, @"cada vez que el JUGADOR\s+(.*?)(?:\.|\s*Las sumas)", RegexOptions.IgnoreCase | RegexOptions.Singleline);
                var description = descriptionMatch.Success ?
                    descriptionMatch.Groups[1].Value.Trim() :
                    string.Empty;

                var objective = new ContractObjective
                {
                    StartDate = startDate,
                    EndDate = endDate,
                    Amount = amount,
                    CurrencyId = 1,
                    Description = description,
                    IsRepeatable = isRepeatable,
                    TimesAchieved = 0
                };

                objectives.Add(objective);

                Console.WriteLine($"Encontrado objetivo: {startDate:dd/MM/yyyy} - {endDate:dd/MM/yyyy}: ${amount:N2}");
                Console.WriteLine($"Descripción: {objective.Description}");
                Console.WriteLine($"Es repetible: {objective.IsRepeatable}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error procesando objetivo: {ex.Message}");
                Console.WriteLine($"Texto del match: {match.Value}");
            }
        }

        return objectives;
    }

    private DateTime ParseSpanishDate(string spanishDate)
    {
        var months = new Dictionary<string, int>
        {
            {"enero", 1}, {"febrero", 2}, {"marzo", 3},
            {"abril", 4}, {"mayo", 5}, {"junio", 6},
            {"julio", 7}, {"agosto", 8}, {"septiembre", 9},
            {"octubre", 10}, {"noviembre", 11}, {"diciembre", 12},
        };

        // Limpiar y normalizar la fecha
        spanishDate = spanishDate.ToLower()
            .Replace(" del ", " de ")
            .Replace("  ", " ")
            .Trim();

        var parts = spanishDate.Split(new[] { " de ", " " }, StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length < 3)
        {
            throw new FormatException($"Formato de fecha inválido: {spanishDate}");
        }

        var day = int.Parse(parts[0]);
        var month = months[parts[1].ToLower()];
        var year = int.Parse(parts[parts.Length - 1]);

        return new DateTime(year, month, day);
    }

    private string CleanText(string text)
    {
        if (string.IsNullOrEmpty(text))
        {
            return string.Empty;
        }

        var replacements = new Dictionary<string, string>
        {
            // Caracteres especiales básicos
            { "\u00A1", "i" },     // ¡
            { "\u2010", "-" },     // ‐
            { "\u2013", "-" },     // –
            { "\u2015", "-" },     // ―
            { "\u201C", "\"" },    // " comilla doble de apertura
            { "\u201D", "\"" },    // " comilla doble de cierre
            { "\u2018", "'" },     // ' comilla simple de apertura
            { "\u2019", "'" },     // ' comilla simple de cierre
            { "\u2026", "..." },   // …
            // Vocales acentuadas
            { "\u00ED", "i" },     // í
            { "\u00F3", "o" },     // ó
            { "\u00E1", "a" },     // á
            { "\u00E9", "e" },     // é
            { "\u00FA", "u" },     // ú
            { "\u00F1", "n" },     // ñ
            // Mayúsculas acentuadas
            { "\u00C1", "A" },     // Á
            { "\u00C9", "E" },     // É
            { "\u00CD", "I" },     // Í
            { "\u00D3", "O" },     // Ó
            { "\u00DA", "U" },     // Ú
            { "\u00D1", "N" },     // Ñ
            // Otros caracteres especiales comunes en PDFs
            { "º", "" },           // símbolo de grado
            { "°", "" },           // símbolo de grado alternativo
            { "ª", "a" },          // ordinal femenino
            { "²", "2" },          // superíndice 2
            { "³", "3" },          // superíndice 3
            { "½", "1/2" },        // fracción un medio
            { "¼", "1/4" },        // fracción un cuarto
            { "¾", "3/4" },        // fracción tres cuartos
            { "\u0092", "'" },     // comilla simple alternativa
            { "\u0093", "\"" },    // comilla doble alternativa
            { "\u0094", "\"" },    // comilla doble alternativa
            { "\u0096", "-" },     // guión alternativo
            { "\u0097", "-" },     // guión alternativo
            // Caracteres de control y espaciado
            { "\r", " " },
            { "\n", " " },
            { "\t", " " },
            { "|", " " },
            { "\f", " " },         // form feed
            { "\v", " " },         // vertical tab
            // Símbolos monetarios y matemáticos comunes
            { "€", "EUR" },
            { "£", "GBP" },
            { "¥", "JPY" },
            { "±", "+-" },
            { "×", "x" },
            { "÷", "/" },
        };

        foreach (var replacement in replacements)
        {
            text = text.Replace(replacement.Key, replacement.Value);
        }

        // Limpiar espacios múltiples
        text = Regex.Replace(text, @"\s+", " ");

        // Limpiar caracteres no imprimibles que no estén en el diccionario
        text = Regex.Replace(text, @"[^\x20-\x7E]", " ");

        return text.Trim();
    }
}