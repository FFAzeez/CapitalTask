using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapitalPlacementTaskAPI.Domain.Utility
{
    public class MaxFileSizeAttribute : ValidationAttribute
    {
        private readonly long _maxFileSizeBytes;

        public MaxFileSizeAttribute(long maxFileSizeBytes)
        {
            _maxFileSizeBytes = maxFileSizeBytes;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is IFormFile file)
            {
                if (file.Length > _maxFileSizeBytes)
                {
                    return new ValidationResult($"File size must not exceed {_maxFileSizeBytes / 1024} KB.");
                }
            }

            return ValidationResult.Success;
        }
    }
}
