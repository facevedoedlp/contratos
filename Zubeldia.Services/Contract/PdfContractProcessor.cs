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

    private string CleanText(string text)
    {
        if (string.IsNullOrEmpty(text)) return string.Empty;

        // Normalizar el texto usando un diccionario para evitar problemas con las comillas
        var replacements = new Dictionary<string, string>
            {
                { "\u00A1", "i" },     // ¡
                { "\u2010", "-" },     // ‐
                { "\u2013", "-" },     // –
                { "\u2015", "-" },     // ―
                { "\u201C", "\"" },    // " comilla doble de apertura
                { "\u201D", "\"" },    // " comilla doble de cierre
                { "\u2018", "'" },     // ' comilla simple de apertura
                { "\u2019", "'" },     // ' comilla simple de cierre
                { "\u2026", "..." },   // …
                { "\u00ED", "i" },     // í
                { "\u00F3", "o" },     // ó
                { "\u00E1", "a" },     // á
                { "\u00E9", "e" },     // é
                { "\u00FA", "u" },     // ú
                { "\u00F1", "n" },     // ñ
                { "\r", " " },
                { "|", " " },
                { "\n", " " }
            };

        foreach (var replacement in replacements)
        {
            text = text.Replace(replacement.Key, replacement.Value);
        }

        // Limpiar espacios múltiples
        text = Regex.Replace(text, @"\s+", " ");

        return text.Trim();
    }

    private (Contract contract, List<ContractSalary> salaries) ParseContractData(string pdfText, Contract contract)
    {
        // Extraer fechas del contrato
        ParseContractHeaderData(pdfText, contract);

        // Extraer salarios
        var salaries = ParseSalariesData(pdfText);

        // Extraer Objetivos
        var objetives = ParseObjetivesData(pdfText);

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

    private List<ContractObjective> ParseObjetivesData(string pdfText)
    {
        var objetives = new List<ContractObjective>();

        return objetives;
    }

    private DateTime ParseSpanishDate(string spanishDate)
    {
        var months = new Dictionary<string, int>
        {
            {"enero", 1}, {"febrero", 2}, {"marzo", 3},
            {"abril", 4}, {"mayo", 5}, {"junio", 6},
            {"julio", 7}, {"agosto", 8}, {"septiembre", 9},
            {"octubre", 10}, {"noviembre", 11}, {"diciembre", 12}
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
}