using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuroLab1.Model
{
    public class Neuron
    {
        public Neuron(int attributeCount, float threshold, Type[] types = null)
        {
            Types = new List<Type>();
            Weight = new float[attributeCount];
            Threshold = 30;

            if (types != null)
                for (int i = 0; i < types.Length; i++)
                    Types.Add(types[i]);

            for (int i = 0; i < attributeCount; i++)
                Weight[i] = 1;
        }

        public List<Type> Types { get; private set; }
        public float[] Weight { get; private set; }
        public float Threshold { get; private set; }

        public bool Check(byte[] image)
        {
            if (image.Length != Weight.Length)
                throw new ArgumentOutOfRangeException("Image length out of range.");

            float sum = 0;

            for (int i = 0; i < image.Length; i++)
            {
                sum += image[i] * Weight[i];
            }

            return sum >= Threshold;
        }

        public void CheckRange(List<byte[]> images)
        {
            bool[] results = new bool[images.Count];

            for (int i = 0; i < images.Count; i++)
            {
                results[i] = Check(images[i]);
            }

            try
            {
                StreamWriter streamWriter = new StreamWriter("Log.txt");

                for (int i = 0; i < results.Length; i++)
                {
                    streamWriter.WriteLine(results[i]);
                }

                streamWriter.Close();
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        public int MisInterpretationRange(List<Type> typesRange, Type targetType)
        {
            int misInterpretationCount = 0;

            for (int i = 0; i < typesRange.Count; i++)
            {
                float sum = 0;

                for (int k = 0; k < typesRange[i].Image.Length; k++)
                {
                    if (typesRange[i].Image.Length != Weight.Length)
                        throw new ArgumentOutOfRangeException("Images length doesn't match.");

                    sum += typesRange[i].Image[k] * Weight[k];
                }

                if ((sum >= Threshold && !(typesRange[i].GetType() == targetType.GetType()))
                        || (sum < Threshold && typesRange[i].GetType() == targetType.GetType()))
                {
                    misInterpretationCount++;
                }
            }

            return misInterpretationCount;
        }

        public void Learn(List<Type> examples, Type targetType, int iterationCount)
        {
            for (int i = 0; i < iterationCount; i++)
            {
                for (int k = 0; k < examples.Count; k++)
                {
                    if (examples[k].Image.Length != Weight.Length)
                        throw new ArgumentOutOfRangeException("Images length doesn't match.");

                    float sum = 0;

                    for (int j = 0; j < Weight.Length; j++)
                        sum += examples[k].Image[j] * Weight[j];

                    if (sum >= Threshold && !(examples[k].GetType() == targetType.GetType()))
                        for (int j = 0; j < Weight.Length; j++)
                            Weight[j] -= examples[k].Image[j];

                    if (sum < Threshold && examples[k].GetType() == targetType.GetType())
                        for (int j = 0; j < Weight.Length; j++)
                            Weight[j] += examples[k].Image[j];
                }
            }
        }
    }
}