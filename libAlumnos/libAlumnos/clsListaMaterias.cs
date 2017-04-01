using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libAlumnos
{
    public class clsListaMaterias : IList<clsMaterias>
    {
        public List<clsMaterias> lstMaterias = new List<clsMaterias>();

        public clsMaterias this[int index]
        {
            get
            {
                return lstMaterias[index];
            }

            set
            {
                lstMaterias[index] = value;
            }
        }

        public int Count
        {
            get
            {
                return lstMaterias.Count;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public void Add(clsMaterias item)
        {
            lstMaterias.Add(item);
        }

        public void Clear()
        {
            lstMaterias.Clear();
        }

        public bool Contains(clsMaterias item)
        {
            return lstMaterias.Contains(item);
        }

        public void CopyTo(clsMaterias[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<clsMaterias> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public int IndexOf(clsMaterias item)
        {
            throw new NotImplementedException();
        }

        public void Insert(int index, clsMaterias item)
        {
            lstMaterias.Insert(index,item);
        }

        public bool Remove(clsMaterias item)
        {
            return lstMaterias.Remove(item);
        }

        public void RemoveAt(int index)
        {
            lstMaterias.RemoveAt(index);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
