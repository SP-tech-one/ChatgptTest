using RegistrationApp.Application.Abstractions;
using RegistrationApp.Application.Contracts.Forms;
using RegistrationApp.Domain.Registrations.ValueObjects;

namespace RegistrationApp.Infrastructure.Registrations;

public sealed class RegistrationFormDefinitionProvider : IRegistrationFormDefinitionProvider
{
    public RegistrationFormDefinition GetDefinition()
    {
        var genderOptions = Gender.GetDisplayOptions()
            .Select(option => new FormFieldOption(option.Key, option.Value))
            .ToList();

        var personalSection = new FormSectionDefinition
        {
            Title = "اطلاعات فردی",
            Description = "فرم زیر را تکمیل کنید تا در حافظه‌ی مرورگر ذخیره شود.",
            Fields = new List<FormFieldDefinition>
            {
                new()
                {
                    Id = "firstName",
                    Name = "firstName",
                    Label = "نام",
                    Type = "text",
                    IsRequired = true,
                    ColumnSpan = 12,
                    CssClass = "mb-3",
                },
                new()
                {
                    Id = "lastName",
                    Name = "lastName",
                    Label = "نام خانوادگی",
                    Type = "text",
                    IsRequired = true,
                    ColumnSpan = 12,
                    CssClass = "mb-3",
                },
                new()
                {
                    Id = "email",
                    Name = "email",
                    Label = "ایمیل",
                    Type = "email",
                    IsRequired = true,
                    ColumnSpan = 12,
                    CssClass = "mb-3",
                },
                new()
                {
                    Id = "phone",
                    Name = "phone",
                    Label = "شماره تماس",
                    Type = "tel",
                    Placeholder = "09xxxxxxxxx",
                    ColumnSpan = 12,
                    CssClass = "mb-3",
                },
                new()
                {
                    Id = "birthDate",
                    Name = "birthDate",
                    Label = "تاریخ تولد",
                    Type = "date",
                    ColumnSpan = 6,
                    CssClass = "mb-3",
                },
                new()
                {
                    Id = "gender",
                    Name = "gender",
                    Label = "جنسیت",
                    Type = "select",
                    ColumnSpan = 6,
                    CssClass = "mb-3",
                    Options = new[] { new FormFieldOption(string.Empty, "انتخاب کنید") }.Concat(genderOptions).ToList(),
                },
                new()
                {
                    Id = "city",
                    Name = "city",
                    Label = "شهر",
                    Type = "text",
                    ColumnSpan = 12,
                    CssClass = "mb-3",
                },
                new()
                {
                    Id = "address",
                    Name = "address",
                    Label = "آدرس کامل",
                    Type = "textarea",
                    ColumnSpan = 12,
                    CssClass = "mb-3",
                },
            }
        };

        return new RegistrationFormDefinition
        {
            Title = "ثبت‌نام",
            Description = personalSection.Description,
            Sections = new[] { personalSection }
        };
    }
}
