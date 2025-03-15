namespace GymShopBlazor.Helpers;
public static class Translations
{
    public static string GetTranslatedCategoryName(string categoryName)
    {
        Dictionary<string, string> categoryTranslations = new()
        {
            { "Clothes", "Träningskläder" },
            { "Supplements", "Kosttillskott" },
            { "Equipments", "Träningstillbehör" },
        };

        if (categoryTranslations.ContainsKey(categoryName))
        {
            return categoryTranslations[categoryName];
        }
        return categoryName;
    }
    public static string GetTranslatedProductStatus(string produtstatus)
    {
        Dictionary<string, string> statusTranslations = new()
        {
            { "Available", "Tillgänglig" },
            { "Out of Stock", "Slut i lager" },
            { "Discontinued", "Utgången" },
            { "Pre-Order", "Förbeställning" },
            { "Limited Edition", "Begränsad upplaga" }
        };

        if (statusTranslations.ContainsKey(produtstatus))
        {
            return statusTranslations[produtstatus];
        }
        return produtstatus;
    }

    public static string GetTranslatedOrderStatus(string orderstatus)
    {
        Dictionary<string, string> statusTranslations = new()
        {
            { "Pending", "Väntande" },
            { "Processing", "Behandlas" },
            { "Completed", "Avslutad" }
        };
        if (statusTranslations.ContainsKey(orderstatus))
        {
            return statusTranslations[orderstatus];
        }
        return orderstatus;
    }
}
