namespace Exercis44
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Student student = new Student() { Id = 1, Name = "Aram", Surname = "Danielyan" };
            Student student1 = new Student() { Id = 2, Name = "Andranik", Surname = "Danielyan" };
            Animal animal = new Animal() { Id = "1", Name = "Charli", Type = "Archavka" };
            Animal animal2 = new Animal() { Id = "2", Name = "Kasper", Type = "Bigle" };
            GenericRepasitory<Student, int> studentRepasitory = new GenericRepasitory<Student, int>();
            studentRepasitory.Add(student);
            studentRepasitory.Add(student1);
            GenericRepasitory<Animal, string> animalRepasitory = new GenericRepasitory<Animal, string>();
            animalRepasitory.Add(animal);
            animalRepasitory.Add(animal2);

          List<Student> _students =  studentRepasitory.GetAll();
          List<Animal> _animals = animalRepasitory.GetAll();
            foreach (var item in _students)
            {
                Console.WriteLine(item.Name + " " + item.Surname);
            }
            student.Surname = "Grigoryan";
            student.Name = "Armen";
            studentRepasitory.Update(student, 1);
            _students = studentRepasitory.GetAll();
            studentRepasitory.Delete(1);
            foreach (var item in _students)
            {
                Console.WriteLine(item.Name + " " + item.Surname);
            }
        }
    }
    class BaseClass<TType>
    { 
     public TType Id { get; set; }
    }

    interface IGenericRepasitory<T,TType> where T : BaseClass<TType>
    {
        void Add(T entity);
        void Delete(TType entity);
        void Update(T entity,TType id);
        T GetById(TType id);
        List<T> GetAll();
    }

    class GenericRepasitory<T,TType> : IGenericRepasitory<T,TType> where T : BaseClass<TType>
    {
        public List<T> _ArrayList = new List<T>();
        public void Add(T entity)
        {
            if (entity != null)
            {
                _ArrayList.Add(entity);
                return;
            }
            throw new Exception("Entity is null");
        }

        public void Delete(TType entity)
        {
            if (entity!=null)
            {
                var item = _ArrayList.FirstOrDefault(x => x.Id.Equals(entity));
                _ArrayList.Remove(item);
                return;
            }
            throw new Exception("Entity is null");
        }

        public List<T> GetAll()
        {
           return _ArrayList;
        }

        public T GetById(TType id)
        {
            if (id == null)
            {
                throw new Exception("Id is null");
            }
            return _ArrayList.FirstOrDefault(x => x.Id.Equals(id));
        }

        public void Update(T entity, TType id)
        {
            var item = GetById(id);
            if (item != null)
            {
                item = entity;
                return;
            }
            throw new Exception("Entity is null");
        }
    }

    class Student : BaseClass<int>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
    class Car : BaseClass<Guid>
    {
        public string Mark { get; set; }
        public string Model { get; set; }
    }
    class Animal : BaseClass<string>
    {
        public string Type { get; set; }
        public string Name { get; set; }
    }
}
