using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkdownTemplating.ViewModels
{
    public class MainViewModel : ViewModel
    {
        public TemplateViewModel Template { get; set; } = new(new Models.Template());
    }
}
