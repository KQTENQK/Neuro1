using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuroLab1.Model
{
    public class Perceptron
    {
        public Perceptron(int attributeCount, Type[] types = null)
        {
            Types = new List<Type>();
            Weights = new List<float[]>();
            Thresholds = new List<float>();

            if (types != null)
                for (int i = 0; i < types.Length; i++)
                    Types.Add(types[i]);

            for (int i = 0; i < types.Length; i++)
            {
                Weights.Add(new float[attributeCount]);

                for (int k = 0; k < attributeCount; k++)
                    Weights[i][k] = 1;
            }

            Thresholds.Add(30);
        }

        public List<Type> Types { get; private set; }
        public List<float[]> Weights { get; private set; }
        public List<float> Thresholds { get; private set; }
        public void Learn(List<Type> examples, Type targetType)
        {
            for (int i = 0; i < examples.Count; i++)
            {
                for (int j = 0; j < Weights.Count; j++)
                {
                    if (examples[i].Image.Length != Weights[j].Length)
                        throw new ArgumentOutOfRangeException("Images length doesn't match.");

                    float sum = 0;

                    for (int k = 0; k < Weights[j].Length; k++)
                        sum += examples[i].Image[k] * Weights[j][k];

                    if (sum >= Thresholds.First() && !examples[i].IsTypeOf(targetType))
                        for (int k = 0; k < Weights[j].Length; k++)
                            Weights[j][k] -= examples[i].Image[k] * (-1);

                    if (sum < Thresholds.First() && examples[i].IsTypeOf(targetType))
                        for (int k = 0; k < Weights[j].Length; k++)
                            Weights[j][k] += examples[i].Image[k];
                }
            }
        }
    }
}
