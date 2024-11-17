namespace Full_GRASP_And_SOLID
{
    public class TimerAdapter : TimerClient
    {
        private Recipe recipe;
        

        public TimerAdapter(Recipe recipe)
        {
            this.recipe = recipe;
        }

        public void TimeOut()
        {
            this.recipe.TimeOut();
        }
    }
}