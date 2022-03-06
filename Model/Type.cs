using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuroLab1.Model
{
    public abstract class Type
    {
        public byte[] Image { get; protected set; }

        public bool IsTypeOf(Type type)
        {
            for (int i = 0; i < type.Image.Length; i++)
            {
                if (Image[i] != type.Image[i])
                    return false;
            }

            return true;
        }

        public Type ChangeImageRandomly(int count)
        {
            Random random = new Random();

            List<int> positions = new List<int>();

            for (int i = 0; i < count; i++)
            {
                int position;
                do
                {
                    position = random.Next(0, Image.Length);
                } while (positions.Contains(position));

                positions.Add(position);
                Image[position] = (byte)random.Next(0, 2);
            }

            return this;
        }
    }
}
