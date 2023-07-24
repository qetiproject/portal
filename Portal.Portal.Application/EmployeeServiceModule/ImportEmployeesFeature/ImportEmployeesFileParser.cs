using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using Portal.Portal.Application.EmployeeServiceModule.Models.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Portal.Application.EmployeeServiceModule.ImportEmployeesFeature
{
    public class ImportEmployeesFileParser
    {
        public List<ImportEmployeeBusinessProcessAction> Parse(IFormFile file)
        {
            var result = new List<ImportEmployeeBusinessProcessAction>();
            using (var stream = new MemoryStream())
            {
                file.CopyTo(stream);
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using (var package = new ExcelPackage(stream))
                {
                    var workSheet = package.Workbook.Worksheets.FirstOrDefault();

                    for (int row = 3; row <= workSheet.Dimension.Rows; row++)
                    {
                        if (TryParseCreateEmployeeBusinessProcessAction(workSheet, row, out ImportEmployeeBusinessProcessAction employee))
                        {
                            result.Add(employee);
                        }
                    }
                }
            }

            return result;
        }

        private bool TryParseCreateEmployeeBusinessProcessAction(ExcelWorksheet worksheet, int row, out ImportEmployeeBusinessProcessAction employee)
        {
            employee = null;

            if (!Enum.TryParse(worksheet.Cells[row, 7].Value?.ToString().Trim(), out JobType jobType))
            {
                return false;
            }

            if (!DateTime.TryParseExact(worksheet.Cells[row, 5].Text, new[] { "dd.MM.yyyy", "dd/MM/yyyy", "dd-MM-yyyy" }, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dateOfBirth))
            {
                if (!DateTime.TryParse(worksheet.Cells[row, 5].Value?.ToString().Trim(), out dateOfBirth))
                {
                    return false;
                }
            }

            employee = new ImportEmployeeBusinessProcessAction(
                worksheet.Cells[row, 1].Value?.ToString().Trim(),
                worksheet.Cells[row, 2].Value?.ToString().Trim(),
                worksheet.Cells[row, 3].Value?.ToString().Trim(),
                worksheet.Cells[row, 4].Value?.ToString().Trim(),
                dateOfBirth,
                worksheet.Cells[row, 6].Value?.ToString().Trim(),
                jobType,
                worksheet.Cells[row, 8].Value?.ToString().Trim(),
                worksheet.Cells[row, 9].Value?.ToString().Trim(),
                ParseNullableDateTime(worksheet.Cells[row, 10].Value?.ToString().Trim()),
                worksheet.Cells[row, 11].Value?.ToString().Trim(),
                ParseNullableBank(worksheet.Cells[row, 12].Value?.ToString().Trim()),
                worksheet.Cells[row, 13].Value?.ToString().Trim(),
                ParseNullableResidency(worksheet.Cells[row, 14].Value?.ToString().Trim()),
                ParseNullableParticipationInPension(worksheet.Cells[row, 15].Value?.ToString().Trim()),
                ParseNullableDecimal(worksheet.Cells[row, 16].Value?.ToString().Trim()),
                ParseNullableDateTime(worksheet.Cells[row, 17].Value?.ToString().Trim()),
                ParseNullableGender(worksheet.Cells[row, 18].Value?.ToString().Trim()),
                ParseNullableMaritalStatus(worksheet.Cells[row, 19].Value?.ToString().Trim()),
                worksheet.Cells[row, 20].Value?.ToString().Trim(),
                worksheet.Cells[row, 21].Value?.ToString().Trim(),
                ParseNullableConviction(worksheet.Cells[row, 22].Value?.ToString().Trim()),
                worksheet.Cells[row, 23].Value?.ToString().Trim(),
                worksheet.Cells[row, 24].Value?.ToString().Trim(),
                worksheet.Cells[row, 25].Value?.ToString().Trim()
            );

            return true;
        }
        private DateTime? ParseNullableDateTime(string value)
        {
            if (!DateTime.TryParseExact(value, new[] { "dd.MM.yyyy", "dd/MM/yyyy", "dd-MM-yyyy" }, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime result))
            {
                if (!DateTime.TryParse(value, out result))
                {
                    return null;
                }
            }

            return result;
        }

        private Bank? ParseNullableBank(string value)
        {
            if (Enum.TryParse(value, out Bank result))
                return result;

            return null;
        }

        private Residency? ParseNullableResidency(string value)
        {
            if (Enum.TryParse(value, out Residency result))
                return result;

            return null;
        }

        private ParticipationInPension? ParseNullableParticipationInPension(string value)
        {
            if (Enum.TryParse(value, out ParticipationInPension result))
                return result;

            return null;
        }

        private decimal? ParseNullableDecimal(string value)
        {
            if (decimal.TryParse(value, out decimal result))
                return result;

            return null;
        }

        private Gender? ParseNullableGender(string value)
        {
            if (Enum.TryParse(value, out Gender result))
                return result;

            return null;
        }

        private MaritalStatus? ParseNullableMaritalStatus(string value)
        {
            if (Enum.TryParse(value, out MaritalStatus result))
                return result;

            return null;
        }

        private Conviction? ParseNullableConviction(string value)
        {
            if (Enum.TryParse(value, out Conviction result))
                return result;

            return null;
        }
    }
}
