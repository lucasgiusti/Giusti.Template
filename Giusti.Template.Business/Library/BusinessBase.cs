using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Giusti.Template.Model.Results;
using Microsoft.Practices.EnterpriseLibrary.Validation;

namespace Giusti.Template.Business.Library
{
    public abstract class BusinessBase
    {
        public ServiceResult serviceResult = new ServiceResult();
        protected void ValidateService(object entity)
        {
            ValidationFactory.ResetCaches();
            Validator validator = ValidationFactory.CreateValidator(entity.GetType());
            ValidationResults results = validator.Validate(entity);
            AddValidationResults(results);
        }
        protected virtual void AddValidationResults(ValidationResults results)
        {
            Messages message;

            foreach (ValidationResult result in results)
            {
                message = new Messages();
                message.Description.Add(result.Message);
                serviceResult.Messages.Add(message);
                serviceResult.Success = false;
            }
        }
        protected void LimpaValidacao()
        {
            serviceResult = new ServiceResult();
        }
        public bool IsValid()
        {
            return serviceResult.Success;
        }
    }
}
