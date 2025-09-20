using System.Linq;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RegistrationApp.Application.Abstractions;
using RegistrationApp.Application.Contracts.Forms;

namespace RegistrationApp.Web.Pages;

public sealed class IndexModel : PageModel
{
    private readonly IRegistrationFormDefinitionProvider _definitionProvider;
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(IRegistrationFormDefinitionProvider definitionProvider, ILogger<IndexModel> logger)
    {
        _definitionProvider = definitionProvider;
        _logger = logger;
    }

    public RegistrationFormDefinition FormDefinition { get; private set; } = default!;

    public void OnGet()
    {
        FormDefinition = _definitionProvider.GetDefinition();
        _logger.LogDebug("Registration form definition resolved with {FieldCount} fields", FormDefinition.Sections.Sum(section => section.Fields.Count));
    }
}
