using System;
using System.Collections.Generic;

namespace Full_GRASP_And_SOLID
{
    
    //Se utiliza SRP
    //Se utiliza DIP
    //Se utiliza el Patron Adapter con TimeAdapter
    public class Recipe : IRecipeContent, TimerClient
    {
        private IList<BaseStep> steps = new List<BaseStep>();

        private bool cooked = false;

        public bool Cooked
        {
            get
            {
                return this.cooked;
            }
        }

        public Product FinalProduct { get; set; }

        public void AddStep(Product input, double quantity, Equipment equipment, int time)
        {
            Step step = new Step(input, quantity, equipment, time);
            this.steps.Add(step);
        }

        public void AddStep(string description, int time)
        {
            WaitStep step = new WaitStep(description, time);
            this.steps.Add(step);
        }

        public void RemoveStep(BaseStep step)
        {
            this.steps.Remove(step);
        }

        public string GetTextToPrint()
        {
            string result = $"Receta de {this.FinalProduct.Description}:\n";
            foreach (BaseStep step in this.steps)
            {
                result = result + step.GetTextToPrint() + "\n";
            }

            result = result + $"Costo de producción: {this.GetProductionCost()}";

            return result;
        }

        public double GetProductionCost()
        {
            double result = 0;

            foreach (BaseStep step in this.steps)
            {
                result = result + step.GetStepCost();
            }

            return result;
        }

        public int GetCookTime()
        {
            int result = 0;

            foreach (BaseStep step in this.steps)
            {
                result = result + step.Time;
            }

            return result;
        }

        public void Cook()
        {
            CountdownTimer timer = new CountdownTimer();
            timer.Register(this.GetCookTime() * 1000, this);
        }

        public void TimeOut()
        {
            this.cooked = true;
        }
    }
}