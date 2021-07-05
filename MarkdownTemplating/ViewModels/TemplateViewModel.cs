using MarkdownTemplating.Models;
using MarkdownTemplating.Windows.Input;
using Microsoft.ClearScript.V8;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Markdig;
using System.Text.RegularExpressions;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Net;

namespace MarkdownTemplating.ViewModels
{
    public class TemplateViewModel : TypedViewModel<Template>
    {
        public TemplateViewModel(Template Model) : base(Model)
        {
            PropertyChanged += TemplateViewModel_PropertyChanged;
        }

        private void TemplateViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(Content):
                    if (DeferPreviewTaskCancellationTokenSource != null)
                        DeferPreviewTaskCancellationTokenSource.Cancel();

                    DeferPreviewTaskCancellationTokenSource = new();
                    DeferPreviewTaskCancellationToken = DeferPreviewTaskCancellationTokenSource.Token;

                    DeferPreviewTask = Task.Run(() =>
                    {
                        for (int i = 0; i < 7; i++)
                        {
                            DeferPreviewTaskCancellationToken.ThrowIfCancellationRequested();
                            Thread.Sleep(100);
                        }

                        UpdatePreviewCommand.Execute(null);

                    }, DeferPreviewTaskCancellationToken);
                    break;
            }
        }

        private Task DeferPreviewTask { get; set; }
        private CancellationTokenSource DeferPreviewTaskCancellationTokenSource { get; set; }
        private CancellationToken DeferPreviewTaskCancellationToken { get; set; }
        public string Content { get => Model.Content; set => Model.Content = value; }
        public string PreviewSource { get; private set; }
        public DummyITSMItem System { get; } = new();
        private static Random rnd { get; } = new();
        public List<DummyITSMItem> Systems { get; } = Enumerable.Repeat(string.Empty, 50)
            .Select(x => new DummyITSMItem() { 
                Hostname = rnd.Next(1118481, 16777215).ToString("X"),
                IPAddress = new IPAddress(Enumerable.Repeat(string.Empty, 4).Select(x => (byte)rnd.Next(0, 256)).ToArray())
            })
            .ToList();

        public ICommand UpdatePreviewCommand => new RelayCommand(
            () =>
            {
                var ScriptEngine = new V8ScriptEngine();
                ScriptEngine.AddHostType(typeof(Enumerable));
                var cnt = Content;

                // Setup the script engine
                ScriptEngine.Script["sys"] = System;
                ScriptEngine.Script["list"] = Systems.ToArray();

                // Replace all expression placeholders with their evaluated result (or the text "error" if the script fails)
                cnt = Regex.Replace(cnt, @"(?s){{(?<expression>.*?)}}", m =>
                {
                    var exp = m.Groups.Cast<Group>().FirstOrDefault(g => g.Name == "expression")?.Value;

                    if (exp == null)
                        return string.Empty;

                    try
                    {
                        return ScriptEngine.Evaluate(exp)?.ToString() ?? string.Empty;
                    }
                    catch (Exception ex)
                    {
                        return $"Error: {ex.Message}";
                    }
                });

                cnt = Regex.Replace(cnt, @"\\{\\{", "{{");
                cnt = Regex.Replace(cnt, @"\\}\\}", "}}");

                // Convert the content from markdown to HTML
                var markdownPipeline = new MarkdownPipelineBuilder()
                    .UseAdvancedExtensions()
                    .Build();

                try
                {
                    cnt = Markdown.ToHtml(cnt, markdownPipeline);
                }
                catch (Exception ex)
                {
                    cnt = ex.Message;
                }

                PreviewSource = cnt;
            },
            () => true
        );
    }
}
