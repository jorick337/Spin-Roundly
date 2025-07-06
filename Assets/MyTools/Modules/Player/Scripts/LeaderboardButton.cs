using MyTools.UI.Objects.Buttons;

namespace MyTools.Start.Buttons
{
    public class LeaderboardButton : BaseButton
    {
        protected override void OnButtonPressed()
        {
            base.OnButtonPressed();
            Load();
        }

        private void Load() {}
    }
}