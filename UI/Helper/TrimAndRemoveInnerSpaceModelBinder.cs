using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.RegularExpressions;

namespace UI.Helper
{
    public class TrimAndRemoveInnerSpaceModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            if (valueProviderResult != ValueProviderResult.None)
            {
                var value = valueProviderResult.FirstValue;
                if (!string.IsNullOrEmpty(value))
                {
                    value = Regex.Replace(value, @"\s+", " ").Trim();
                }
                bindingContext.Result = ModelBindingResult.Success(value);
            }
            return Task.CompletedTask;
        }
    }
}
