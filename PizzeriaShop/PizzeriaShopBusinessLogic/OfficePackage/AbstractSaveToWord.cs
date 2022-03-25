using PizzeriaShopBusinessLogic.OfficePackage.HelperEnums;
using PizzeriaShopBusinessLogic.OfficePackage.HelperModels;
using System.Collections.Generic;

namespace PizzeriaShopBusinessLogic.OfficePackage
{
    public abstract class AbstractSaveToWord
    {
        public void CreateDoc(WordInfo info)
        {
            CreateWord(info);

            CreateParagraph(new WordParagraph
            {
                Texts = new List<(string, WordTextProperties)>
                {(
                info.Title, new WordTextProperties
                {
                    Bold = true, Size = "24",
                }
                )},
                TextProperties = new WordTextProperties
                {
                    Size = "24",
                    JustificationType = WordJustificationType.Center
                }
            });

            foreach (var pizza in info.Pizzas)
            {
                CreateParagraph(new WordParagraph
                {
                    Texts = new List<(string, WordTextProperties)>
                    {
                        (pizza.PizzaName + ": ", new WordTextProperties { Size = "24", Bold = true }),
                        (pizza.Price.ToString(), new WordTextProperties { Size = "24", Bold = false })
                    },
                    TextProperties = new WordTextProperties
                    {
                        Size = "24",
                        JustificationType = WordJustificationType.Both
                    }
                });
            }

            SaveWord(info);
        }

        protected abstract void CreateWord(WordInfo info);

        protected abstract void CreateParagraph(WordParagraph paragraph);

        protected abstract void SaveWord(WordInfo info);
    }
}
