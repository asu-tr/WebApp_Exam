using HtmlAgilityPack;
using HtmlAgilityPack.CssSelectors.NetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp_Exam.Models;

namespace WebApp_Exam.Pages
{
    [Authorize]
    public class CreateExamModel : PageModel
    {
        private readonly string url = "https://www.wired.com/";
        public List<WiredText> WiredTexts { get; set; } = new List<WiredText>();
        public Exam NewExam { get; set; }


        public void OnGet()
        {
            this.WiredTexts = GetAllTextInfo();
        }

        private List<WiredText> GetAllTextInfo()
        {
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(url);

            // i get the divs with the class i want
            IList<HtmlNode> nodes = doc.QuerySelectorAll("div .summary-list__items");
            // most recent table is second table on this page so i select it
            HtmlNode recentsNode = nodes[1];
            // for every text i get it's own node
            IList<HtmlNode> recents = recentsNode.QuerySelectorAll("div .SummaryItemWrapper-gdEuvf a .SummaryItemHedLink-cgPsOZ");

            List<WiredText> tempList = new List<WiredText>();

            //getting text details from each node
            foreach (HtmlNode node in recents)
            {
                tempList.Add(new WiredText
                {
                    Title = node.QuerySelector("h2").InnerText,
                    // i'll get the texts later.
                    Text = "",
                    // href value doesn't have the website so i added it manually.
                    TextUrl = url.Substring(0, url.Length - 1) + node.Attributes["href"].Value
                });
            }

            // add texts to WiredTexts from their url
            foreach (WiredText text in tempList)
            {
                HtmlDocument doc2 = web.Load(text.TextUrl);
                HtmlNode node = doc2.QuerySelector("div .BodyWrapper-ctnerm p");

                text.Text = node.InnerHtml;
            }

            return tempList;
        } 
    }
}
