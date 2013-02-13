using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

/// <summary>
/// Summary description for UserProfile
/// </summary>
public class UserProfile
{
    public static string getListItemValueById(int id, List<ListItem> targetList)
    {
        ListItem value = new ListItem();

        foreach (ListItem item in targetList)
        {
            if (item.Id == id)
            {
                value = item;
            }
        }

        return value.Name;
    }

    public static List<ListItem> ListGender()
    {
        List<ListItem> list = new List<ListItem>();
        list.Add(new ListItem("Male", 0));
        list.Add(new ListItem("Female", 1));
        return list;
    }

    public static string GetGender(string id)
    {
        return getListItemValueById(Convert.ToInt32(id), ListGender());
    }

    public static List<ListItem> ListEthnicity()
    {
        List<ListItem> list = new List<ListItem>();
        list.Add(new ListItem("African", 0));
        list.Add(new ListItem("African American", 1));
        list.Add(new ListItem("Pacific Islander", 2));
        list.Add(new ListItem("Caucasian", 3));
        list.Add(new ListItem("European", 4));
        list.Add(new ListItem("Hispanic", 5));
        list.Add(new ListItem("Indian", 6));
        list.Add(new ListItem("Middle Eastern", 7));
        list.Add(new ListItem("Native American", 8));
        list.Add(new ListItem("Asian", 9));
        list.Add(new ListItem("Biracial", 10));
        list.Add(new ListItem("Other Ethnicity", 11));
        return list;
    }

    public static string GetEthnicity(string id)
    {
        return getListItemValueById(Convert.ToInt32(id), ListEthnicity());
    }

    public static List<ListItem> ListState()
    {
        List<ListItem> list = new List<ListItem>();
        list.Add(new ListItem("Alabama", 0));
        list.Add(new ListItem("Alaska", 1));
        list.Add(new ListItem("American Samoa", 2));
        list.Add(new ListItem("Arizona", 3));
        list.Add(new ListItem("Arkansas", 4));
        list.Add(new ListItem("Armed Forces Americas", 5));
        list.Add(new ListItem("Armed Forces Europe", 6));
        list.Add(new ListItem("Armed Forces Pacific", 7));
        list.Add(new ListItem("California", 8));
        list.Add(new ListItem("Colorado", 9));
        list.Add(new ListItem("Connecticut", 10));
        list.Add(new ListItem("Delaware", 11));
        list.Add(new ListItem("District Of Columbia", 12));
        list.Add(new ListItem("Florida", 13));
        list.Add(new ListItem("Georgia", 14));
        list.Add(new ListItem("Guam", 15));
        list.Add(new ListItem("Hawaii", 16));
        list.Add(new ListItem("Idaho", 17));
        list.Add(new ListItem("Illinois", 18));
        list.Add(new ListItem("Indiana", 19));
        list.Add(new ListItem("Iowa", 20));
        list.Add(new ListItem("Kansas", 21));
        list.Add(new ListItem("Kentucky", 22));
        list.Add(new ListItem("Louisiana", 23));
        list.Add(new ListItem("Maine", 24));
        list.Add(new ListItem("Maryland", 25));
        list.Add(new ListItem("Massachusetts", 26));
        list.Add(new ListItem("Michigan", 27));
        list.Add(new ListItem("Minnesota", 28));
        list.Add(new ListItem("Mississippi", 29));
        list.Add(new ListItem("Missouri", 30));
        list.Add(new ListItem("Montana", 31));
        list.Add(new ListItem("Nebraska", 32));
        list.Add(new ListItem("Nevada", 33));
        list.Add(new ListItem("New Hampshire", 34));
        list.Add(new ListItem("New Jersey", 35));
        list.Add(new ListItem("New Mexico", 36));
        list.Add(new ListItem("New York", 37));
        list.Add(new ListItem("North Carolina", 38));
        list.Add(new ListItem("North Dakota", 39));
        list.Add(new ListItem("Northern Mariana Is", 40));
        list.Add(new ListItem("Ohio", 41));
        list.Add(new ListItem("Oklahoma", 42));
        list.Add(new ListItem("Oregon", 43));
        list.Add(new ListItem("Palau", 44));
        list.Add(new ListItem("Pennsylvania", 45));
        list.Add(new ListItem("Puerto Rico", 46));
        list.Add(new ListItem("Rhode Island", 47));
        list.Add(new ListItem("South Carolina", 48));
        list.Add(new ListItem("South Dakota", 49));
        list.Add(new ListItem("Tennessee", 50));
        list.Add(new ListItem("Texas", 51));
        list.Add(new ListItem("Utah", 52));
        list.Add(new ListItem("Vermont", 53));
        list.Add(new ListItem("Virgin Islands", 54));
        list.Add(new ListItem("Virginia", 55));
        list.Add(new ListItem("Washington", 56));
        list.Add(new ListItem("West Virginia", 57));
        list.Add(new ListItem("Wisconsin", 58));
        list.Add(new ListItem("Wyoming", 59));
        list.Add(new ListItem("The Virgin Islands", 60));
        return list;
    }

    public static string GetState(string id)
    {
        return getListItemValueById(Convert.ToInt32(id), ListState());
    }

    public static List<ListItem> ListMaritalStatus()
    {
        List<ListItem> list = new List<ListItem>();
        list.Add(new ListItem("Select Status", 0));
        list.Add(new ListItem("Single", 1));
        list.Add(new ListItem("Married", 2));
        list.Add(new ListItem("Living Together", 3));
        list.Add(new ListItem("Divorced", 4));
        list.Add(new ListItem("Widowed", 5));
        list.Add(new ListItem("Separated", 6));
        list.Add(new ListItem("Not Single/Not Looking", 7));
        return list;
    }

    public static string GetMaritalStatus(string id)
    {
        return getListItemValueById(Convert.ToInt32(id), ListMaritalStatus());
    }

    public static List<ListItem> ListHeight()
    {
        List<ListItem> list = new List<ListItem>();
        list.Add(new ListItem("> 5' (> 152 cm)", 0));
        list.Add(new ListItem("5'0\" (152 cm)", 152));
        list.Add(new ListItem("5'1\" (155 cm)", 155));
        list.Add(new ListItem("5'2\" (157 cm)", 157));
        list.Add(new ListItem("5'3\" (160 cm)", 160));
        list.Add(new ListItem("5'4\" (163 cm)", 163));
        list.Add(new ListItem("5'5\" (165 cm)", 165));
        list.Add(new ListItem("5'6\" (168 cm)", 168));
        list.Add(new ListItem("5'7\" (170 cm)", 170));
        list.Add(new ListItem("5'8\" (173 cm)", 173));
        list.Add(new ListItem("5'9\" (175 cm)", 175));
        list.Add(new ListItem("5'10\" (178 cm)", 178));
        list.Add(new ListItem("5'11\" (180 cm)", 180));
        list.Add(new ListItem("6'0\" (183 cm)", 183));
        list.Add(new ListItem("6'1\" (185 cm)", 185));
        list.Add(new ListItem("6'2\" (188 cm)", 188));
        list.Add(new ListItem("6'3\" (191 cm)", 191));
        list.Add(new ListItem("6'4\" (193 cm)", 193));
        list.Add(new ListItem("6'5\" (196 cm)", 196));
        list.Add(new ListItem("6'6\" (198 cm)", 198));
        list.Add(new ListItem("6'7\" (201 cm)", 201));
        list.Add(new ListItem("6'8\" (203 cm)", 203));
        list.Add(new ListItem("6'9\" (206 cm)", 206));
        list.Add(new ListItem("6'10\" (208 cm)", 208));
        list.Add(new ListItem("6'11\" (211 cm)", 211));
        list.Add(new ListItem("7' 0\" (213 cm)", 213));
        list.Add(new ListItem("< 7' (< 213 cm)", 999));
        return list;
    }

    public static string GetHeight(string id)
    {
        return getListItemValueById(Convert.ToInt32(id), ListHeight());
    }

    public static List<ListItem> ListBodyType()
    {
        List<ListItem> list = new List<ListItem>();
        list.Add(new ListItem("Select Body Type", 0));
        list.Add(new ListItem("Thin", 1));
        list.Add(new ListItem("Athletic", 2));
        list.Add(new ListItem("Average", 3));
        list.Add(new ListItem("A Few Extra Pounds", 4));
        list.Add(new ListItem("Big & Tall/BBW", 5));
        return list;
    }

    public static string GetBodyType(string id)
    {
        return getListItemValueById(Convert.ToInt32(id), ListBodyType());
    }

    public static List<ListItem> ListHairColor()
    {
        List<ListItem> list = new List<ListItem>();
        list.Add(new ListItem("Black", 0));
        list.Add(new ListItem("Blond", 1));
        list.Add(new ListItem("Brown", 2));
        list.Add(new ListItem("Red", 3));
        list.Add(new ListItem("Gray", 4));
        list.Add(new ListItem("Bald", 5));
        list.Add(new ListItem("Mixed Color", 6));
        return list;
    }

    public static string GetHairColor(string id)
    {
        return getListItemValueById(Convert.ToInt32(id), ListHairColor());
    }

    public static List<ListItem> ListEyeColor()
    {
        List<ListItem> list = new List<ListItem>();
        list.Add(new ListItem("Green", 0));
        list.Add(new ListItem("Blue", 1));
        list.Add(new ListItem("Brown", 2));
        return list;
    }

    public static string GetEyeColor(string id)
    {
        return getListItemValueById(Convert.ToInt32(id), ListEyeColor());
    }

    public static List<ListItem> ListLookingFor()
    {
        List<ListItem> list = new List<ListItem>();
        list.Add(new ListItem("Select", 0));
        list.Add(new ListItem("Hang Out", 1));
        list.Add(new ListItem("Talk/E-mail", 2));
        list.Add(new ListItem("Long-term", 3));
        list.Add(new ListItem("Other Relationship", 4));
        list.Add(new ListItem("Dating", 5));
        list.Add(new ListItem("Friends", 6));
        list.Add(new ListItem("Intimate Encounter", 7));
        list.Add(new ListItem("Activity Partner", 8));
        return list;
    }

    public static string GetLookingFor(string id)
    {
        return getListItemValueById(Convert.ToInt32(id), ListLookingFor());
    }

    public static List<ListItem> ListSeeking()
    {
        List<ListItem> list = new List<ListItem>();
        list.Add(new ListItem("Male", 0));
        list.Add(new ListItem("Female", 1));
        return list;
    }

    public static string GetSeeking(string id)
    {
        return getListItemValueById(Convert.ToInt32(id), ListSeeking());
    }

    public static List<ListItem> ListHaveChildren()
    {
        List<ListItem> list = new List<ListItem>();
        list.Add(new ListItem("Select", 0));
        list.Add(new ListItem("Yes", 1));
        list.Add(new ListItem("No", 2));
        list.Add(new ListItem("All my kids are over 18", 3));
        return list;
    }

    public static string GetHaveChildren(string id)
    {
        return getListItemValueById(Convert.ToInt32(id), ListHaveChildren());
    }

    public static List<ListItem> ListWantChildren()
    {
        List<ListItem> list = new List<ListItem>();
        list.Add(new ListItem("Select", 0));
        list.Add(new ListItem("Want children", 1));
        list.Add(new ListItem("Does not want children", 2));
        list.Add(new ListItem("Undecided/Open", 3));
        return list;
    }

    public static string GetWantChildren(string id)
    {
        return getListItemValueById(Convert.ToInt32(id), ListWantChildren());
    }

    public static List<ListItem> ListAlcohol()
    {
        List<ListItem> list = new List<ListItem>();
        list.Add(new ListItem("Select", 0));
        list.Add(new ListItem("No", 1));
        list.Add(new ListItem("Socially", 2));
        list.Add(new ListItem("Often (>3 times/week)", 3));
        return list;
    }

    public static string GetAlcohol(string id)
    {
        return getListItemValueById(Convert.ToInt32(id), ListAlcohol());
    }

    public static List<ListItem> ListSmoking()
    {
        List<ListItem> list = new List<ListItem>();
        list.Add(new ListItem("Select", 0));
        list.Add(new ListItem("No", 1));
        list.Add(new ListItem("Occasionally", 2));
        list.Add(new ListItem("Often", 3));
        return list;
    }

    public static string GetSmoking(string id)
    {
        return getListItemValueById(Convert.ToInt32(id), ListSmoking());
    }

    public static List<ListItem> ListRecreationalDrugs()
    {
        List<ListItem> list = new List<ListItem>();
        list.Add(new ListItem("Select", 0));
        list.Add(new ListItem("No", 1));
        list.Add(new ListItem("Socially", 2));
        list.Add(new ListItem("Often (>3 times/week)", 3));
        return list;
    }

    public static string GetRecreationalDrugs(string id)
    {
        return getListItemValueById(Convert.ToInt32(id), ListRecreationalDrugs());
    }

    public static List<ListItem> ListReligion()
    {
        List<ListItem> list = new List<ListItem>();
        list.Add(new ListItem("Prefer Not To Say", 0));
        list.Add(new ListItem("Anglican", 1));
        list.Add(new ListItem("Baptist", 2));
        list.Add(new ListItem("Buddhist", 3));
        list.Add(new ListItem("Catholic", 4));
        list.Add(new ListItem("Christian - Other", 5));
        list.Add(new ListItem("Hindu", 6));
        list.Add(new ListItem("Islamic", 7));
        list.Add(new ListItem("Jewish", 8));
        list.Add(new ListItem("Lutheran", 9));
        list.Add(new ListItem("Methodist", 10));
        list.Add(new ListItem("New Age", 11));
        list.Add(new ListItem("Non-Religious", 12));
        list.Add(new ListItem("Other", 13));
        list.Add(new ListItem("Presbyterian", 14));
        list.Add(new ListItem("Sikh", 15));
        return list;
    }

    public static string GetReligion(string id)
    {
        return getListItemValueById(Convert.ToInt32(id), ListReligion());
    }

    public static List<ListItem> ListMonths()
    {
        List<ListItem> list = new List<ListItem>();
        list.Add(new ListItem("January", 1));
        list.Add(new ListItem("February", 2));
        list.Add(new ListItem("March", 3));
        list.Add(new ListItem("April", 4));
        list.Add(new ListItem("May", 5));
        list.Add(new ListItem("June", 6));
        list.Add(new ListItem("July", 7));
        list.Add(new ListItem("August", 8));
        list.Add(new ListItem("September", 9));
        list.Add(new ListItem("October", 10));
        list.Add(new ListItem("November", 11));
        list.Add(new ListItem("December", 12));
        return list;
    }

    public static string GetMonth(string id)
    {
        return getListItemValueById(Convert.ToInt32(id), ListMonths());
    }

    public static List<ListItem> ListDays()
    {
        List<ListItem> list = new List<ListItem>();
        list.Add(new ListItem("1", 1));
        list.Add(new ListItem("2", 2));
        list.Add(new ListItem("3", 3));
        list.Add(new ListItem("4", 4));
        list.Add(new ListItem("5", 5));
        list.Add(new ListItem("6", 6));
        list.Add(new ListItem("7", 7));
        list.Add(new ListItem("8", 8));
        list.Add(new ListItem("9", 9));
        list.Add(new ListItem("10", 10));
        list.Add(new ListItem("11", 11));
        list.Add(new ListItem("12", 12));
        list.Add(new ListItem("13", 13));
        list.Add(new ListItem("14", 14));
        list.Add(new ListItem("15", 15));
        list.Add(new ListItem("16", 16));
        list.Add(new ListItem("17", 17));
        list.Add(new ListItem("18", 18));
        list.Add(new ListItem("19", 19));
        list.Add(new ListItem("20", 20));
        list.Add(new ListItem("21", 21));
        list.Add(new ListItem("22", 22));
        list.Add(new ListItem("23", 23));
        list.Add(new ListItem("24", 24));
        list.Add(new ListItem("25", 25));
        list.Add(new ListItem("26", 26));
        list.Add(new ListItem("27", 27));
        list.Add(new ListItem("28", 28));
        list.Add(new ListItem("29", 29));
        list.Add(new ListItem("30", 30));
        list.Add(new ListItem("31", 31));
        return list;
    }

    public static string GetDay(string id)
    {
        return getListItemValueById(Convert.ToInt32(id), ListDays());
    }

    public static List<ListItem> ListYears()
    {
        List<ListItem> list = new List<ListItem>();
        list.Add(new ListItem("1991", 1991));
        list.Add(new ListItem("1990", 1990));
        list.Add(new ListItem("1989", 1989));
        list.Add(new ListItem("1988", 1988));
        list.Add(new ListItem("1987", 1987));
        list.Add(new ListItem("1986", 1986));
        list.Add(new ListItem("1985", 1985));
        list.Add(new ListItem("1984", 1984));
        list.Add(new ListItem("1983", 1983));
        list.Add(new ListItem("1982", 1982));
        list.Add(new ListItem("1981", 1981));
        list.Add(new ListItem("1980", 1980));
        list.Add(new ListItem("1979", 1979));
        list.Add(new ListItem("1978", 1978));
        list.Add(new ListItem("1977", 1977));
        list.Add(new ListItem("1976", 1976));
        list.Add(new ListItem("1975", 1975));
        list.Add(new ListItem("1974", 1974));
        list.Add(new ListItem("1973", 1973));
        list.Add(new ListItem("1972", 1972));
        list.Add(new ListItem("1971", 1971));
        list.Add(new ListItem("1970", 1970));
        list.Add(new ListItem("1969", 1969));
        list.Add(new ListItem("1968", 1968));
        list.Add(new ListItem("1967", 1967));
        list.Add(new ListItem("1966", 1966));
        list.Add(new ListItem("1965", 1965));
        list.Add(new ListItem("1964", 1964));
        list.Add(new ListItem("1963", 1963));
        list.Add(new ListItem("1962", 1962));
        list.Add(new ListItem("1961", 1961));
        list.Add(new ListItem("1960", 1960));
        list.Add(new ListItem("1959", 1959));
        list.Add(new ListItem("1958", 1958));
        list.Add(new ListItem("1957", 1957));
        list.Add(new ListItem("1956", 1956));
        list.Add(new ListItem("1955", 1955));
        list.Add(new ListItem("1954", 1954));
        list.Add(new ListItem("1953", 1953));
        list.Add(new ListItem("1952", 1952));
        list.Add(new ListItem("1951", 1951));
        list.Add(new ListItem("1950", 1950));
        list.Add(new ListItem("1949", 1949));
        list.Add(new ListItem("1948", 1948));
        list.Add(new ListItem("1947", 1947));
        list.Add(new ListItem("1946", 1946));
        list.Add(new ListItem("1945", 1945));
        list.Add(new ListItem("1944", 1944));
        list.Add(new ListItem("1943", 1943));
        list.Add(new ListItem("1942", 1942));
        list.Add(new ListItem("1941", 1941));
        list.Add(new ListItem("1940", 1940));
        list.Add(new ListItem("1939", 1939));
        list.Add(new ListItem("1938", 1938));
        list.Add(new ListItem("1937", 1937));
        list.Add(new ListItem("1936", 1936));
        list.Add(new ListItem("1935", 1935));
        list.Add(new ListItem("1934", 1934));
        list.Add(new ListItem("1933", 1933));
        list.Add(new ListItem("1932", 1932));
        list.Add(new ListItem("1931", 1931));
        list.Add(new ListItem("1930", 1930));
        list.Add(new ListItem("1929", 1929));
        list.Add(new ListItem("1928", 1928));
        list.Add(new ListItem("1927", 1927));
        list.Add(new ListItem("1926", 1926));
        list.Add(new ListItem("1925", 1925));
        list.Add(new ListItem("1924", 1924));
        list.Add(new ListItem("1923", 1923));
        list.Add(new ListItem("1922", 1922));
        list.Add(new ListItem("1921", 1921));
        list.Add(new ListItem("1920", 1920));
        list.Add(new ListItem("1919", 1919));
        list.Add(new ListItem("1918", 1918));
        list.Add(new ListItem("1917", 1917));
        list.Add(new ListItem("1916", 1916));
        list.Add(new ListItem("1915", 1915));
        list.Add(new ListItem("1914", 1914));
        list.Add(new ListItem("1913", 1913));
        list.Add(new ListItem("1912", 1912));
        list.Add(new ListItem("1911", 1911));
        list.Add(new ListItem("1910", 1910));
        list.Add(new ListItem("1909", 1909));
        list.Add(new ListItem("1908", 1908));
        list.Add(new ListItem("1907", 1907));
        list.Add(new ListItem("1906", 1906));
        return list;
    }

    public static string GetYear(string id)
    {
        return getListItemValueById(Convert.ToInt32(id), ListYears());
    }

    public static string GetAge(string BirthDateString)
    {
        DateTime BirthDate = DateTime.Parse(BirthDateString);
        // get the difference in years
        int years = DateTime.Now.Year - BirthDate.Year;
        // subtract another year if we're before the
        // birth day in the current year
        if (DateTime.Now.Month < BirthDate.Month ||
            (DateTime.Now.Month == BirthDate.Month &&
            DateTime.Now.Day < BirthDate.Day))
            years--;

        return years.ToString();
    }
}

public class ListItem
{
    public ListItem() { }

    public ListItem(string name, int id)
    {
        Id = id;
        Name = name;
    }

    public int Id { get; set; }

    public string Name { get; set; }
}