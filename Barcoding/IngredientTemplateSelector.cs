using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barcoding
{
    public class IngredientTemplateSelector : DataTemplateSelector
    {
        public DataTemplate IngredientTemplate { get; set; }
        public DataTemplate SeparatorTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            return item is Models.Ingridients ? IngredientTemplate : SeparatorTemplate;
        }
    }
}
