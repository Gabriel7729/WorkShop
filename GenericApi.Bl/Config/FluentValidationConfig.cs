using FluentValidation.AspNetCore;
using GenericApi.Bl.Validations;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace GenericApi.Bl.Config
{
    public static class FluentValidationConfig
    {
        public static IMvcBuilder ConfigFluentValidation(this IMvcBuilder builder)
        {
            builder.AddFluentValidation(x =>
            {
                x.AutomaticValidationEnabled = false;
                x.RegisterValidatorsFromAssemblyContaining<DocumentValidator>();
            });
            return builder;
        }
    }
}
