using LiveCharts;
using NeuroLab1.Model;
using NeuroLab1.Model.Types;
using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace NeuroLab1.ViewModel
{
    public class MainWindowViewModel
    {
        public MainWindowViewModel()
        {
            Neuron = new Neuron(35, 30);
            BaseValues = new ChartValues<double>();

            for (int i = 0; i < Neuron.Weight.Length; i++)
                BaseValues.Add(Neuron.Weight[i]);

            EndValues = new ChartValues<double>();

            for (int i = 0; i < Neuron.Weight.Length; i++)
                EndValues.Add(Neuron.Weight[i]);
        }

        public void Check(List<byte[]> images)
        {
            Neuron.CheckRange(images);
        }

        public void BeginLearning(int noizeCount = 0)
        {
            Neuron.Learn(GetRandomValues(500, noizeCount), new Zero(), 100);
            int misCount = Neuron.MisInterpretationRange(new List<Model.Type>() {new Zero(), new One(),
                                               new Two(), new Three(),
                                                new Four(), new Five(),
                                                new Six(), new Seven(),
                                                new Eight(), new Nine() }, new Zero());
            float typesCount = 10;
            float misInterpretatingRange = typesCount / misCount;

            if (misCount == 0)
                misInterpretatingRange = 0;

            for (int i = 0; i < Neuron.Weight.Length; i++)
                EndValues[i] = Neuron.Weight[i];

            MisInfo = $"Wrong interpretation range: {misInterpretatingRange} %.";
        }

        private List<Model.Type> GetRandomValues(int range, int noiseCount)
        {
            Random random = new Random();
            List<Model.Type> returnable = new List<Model.Type>();

            for (int i = 0; i < range; i++)
            {
                int randomValue = random.Next(0, 10);
                System.Threading.Thread.Sleep(2);

                switch (randomValue)
                {
                    case 0:
                        returnable.Add(new Zero().ChangeImageRandomly(noiseCount));
                        break;
                    case 1:
                        returnable.Add(new One().ChangeImageRandomly(noiseCount));
                        break;
                    case 2:
                        returnable.Add(new Two().ChangeImageRandomly(noiseCount));
                        break;
                    case 3:
                        returnable.Add(new Three().ChangeImageRandomly(noiseCount));
                        break;
                    case 4:
                        returnable.Add(new Four().ChangeImageRandomly(noiseCount));
                        break;
                    case 5:
                        returnable.Add(new Five().ChangeImageRandomly(noiseCount));
                        break;
                    case 6:
                        returnable.Add(new Six().ChangeImageRandomly(noiseCount));
                        break;
                    case 7:
                        returnable.Add(new Seven().ChangeImageRandomly(noiseCount));
                        break;
                    case 8:
                        returnable.Add(new Eight().ChangeImageRandomly(noiseCount));
                        break;
                    case 9:
                        returnable.Add(new Nine().ChangeImageRandomly(noiseCount));
                        break;
                    default:
                        throw new ArgumentOutOfRangeException("Randomizable range out of [0; 9].");
                }
            }

            return returnable;
        }

        public Neuron Neuron { get; private set; }
        public ChartValues<double> BaseValues { get; private set; }
        public ChartValues<double> EndValues { get; private set; }
        public string MisInfo { get; set; }
    }
}
