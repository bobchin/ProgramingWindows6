namespace HowToCancelAsync
{
    public class DialogButton
    {
        public DialogButton(string title, object color)
        {
            this.Title = title;
            this.Id = color;
        }

        public string Title { set; get; }
        public object Id { set; get; }
    }
}
