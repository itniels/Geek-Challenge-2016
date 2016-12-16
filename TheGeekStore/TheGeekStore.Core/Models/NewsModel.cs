using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGeekStore.Core.Models
{
    [Table("News")]
    public class NewsModel
    {
        public int Id { get; set; }
        public DateTime Time { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }

        public string GetExcerpt(int length = 130)
        {
            if (Text.Length > length)
            {
                var excerpt = Text.Substring(0, length);
                excerpt += "...";
                return excerpt;
            }
            return Text;
        }
    }
}
