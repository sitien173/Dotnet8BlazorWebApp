using FluentValidation;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace BlazorWebApp.Shared;

public class FluentValidator<TValidator> : ComponentBase
{
    private static readonly char[] separators = { '.', '[' };

    [Inject] private IValidator<TValidator> Validator { get; set; } = null!;

    [CascadingParameter] private EditContext EditContext { get; set; } = null!;
    
    protected override Task OnInitializedAsync()
    {
        var messages = new ValidationMessageStore(EditContext);

        // Revalidate when any field changes, or if the entire form requests validation
        // (e.g., on submit)

        EditContext.OnFieldChanged += (sender, eventArgs)
            => ValidateModel((EditContext)sender!, messages);

        EditContext.OnValidationRequested += (sender, eventArgs)
            => ValidateModel((EditContext)sender!, messages);
        
        return Task.CompletedTask;
    }
    
    private void ValidateModel(EditContext editContext, ValidationMessageStore messages)
    {
        var context = new ValidationContext<object>(editContext.Model);
        var validationResult = Validator.ValidateAsync(context).GetAwaiter().GetResult();

        messages.Clear();
        foreach (var error in validationResult.Errors)
        {
            var fieldIdentifier = ToFieldIdentifier(editContext, error.PropertyName);
            messages.Add(fieldIdentifier, error.ErrorMessage);
        }

        editContext.NotifyValidationStateChanged();
    }

    private static FieldIdentifier ToFieldIdentifier(EditContext editContext, string propertyPath)
    {
        // This method parses property paths like 'SomeProp.MyCollection[123].ChildProp'
        // and returns a FieldIdentifier which is an (instance, propName) pair. For example,
        // it would return the pair (SomeProp.MyCollection[123], "ChildProp"). It traverses
        // as far into the propertyPath as it can go until it finds any null instance.

        object obj = editContext.Model;

        while (true)
        {
            int nextTokenEnd = propertyPath.IndexOfAny(separators);
            if (nextTokenEnd < 0)
            {
                return new FieldIdentifier(obj, propertyPath);
            }

            string nextToken = propertyPath[..nextTokenEnd];
            propertyPath = propertyPath[(nextTokenEnd + 1)..];

            object? newObj;
            if (nextToken.EndsWith(']'))
            {
                // It's an indexer
                // This code assumes C# conventions (one indexer named Item with one param)
                nextToken = nextToken[..^1];
                var prop = obj.GetType().GetProperty("Item");
                Type indexerType = prop?.GetIndexParameters()[0].ParameterType!;
                object indexerValue = Convert.ChangeType(nextToken, indexerType);
                newObj = prop?.GetValue(obj, new [] { indexerValue })!;
            }
            else
            {
                // It's a regular property
                var prop = obj.GetType().GetProperty(nextToken);
                if (prop == null)
                {
                    throw new InvalidOperationException(
                        $"Could not find property named {nextToken} on object of type {obj.GetType().FullName}.");
                }

                newObj = prop.GetValue(obj);
            }

            if (newObj == null)
            {
                // This is as far as we can go
                return new FieldIdentifier(obj, nextToken);
            }

            obj = newObj;
        }
    }
}