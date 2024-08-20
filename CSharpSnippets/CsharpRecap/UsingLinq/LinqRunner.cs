namespace CsharpRecap.UsingLinq;

public class LinqRunner
{
    public static void Run()
    {
        //UsingZip();

        //SecondarySort();
        //ChunkData();

        //UseJoin();
        //UseGroup();
        GroupByContiguousKeys.GroupByContiguousKeys1();
    }

    private static void UseGroup()
    {
        var students = new List<Student>();

        // Group then order by key
        var groupByYearQuery =
            from student in students
            group student by student.Year into newGroup
            orderby newGroup.Key
            select newGroup;

        foreach (var yearGroup in groupByYearQuery)
        {
            Console.WriteLine($"Key: {yearGroup.Key}");
            foreach (var student in yearGroup)
            {
                Console.WriteLine($"\t{student.LastName}, {student.FirstName}");
            }
        }

        // Variable groupByLastNamesQuery is an IEnumerable<IGrouping<string,
        // DataClass.Student>>.
        var groupByYearQueryMethodBy = students
            .GroupBy(student => student.Year)
            .OrderBy(newGroup => newGroup.Key);

        foreach (var yearGroup in groupByYearQuery)
        {
            Console.WriteLine($"Key: {yearGroup.Key}");
            foreach (var student in yearGroup)
            {
                Console.WriteLine($"\t{student.LastName}, {student.FirstName}");
            }
        }

        // Group by percentile
        static int GetPercentile(Student s)
        {
            double avg = s.Scores.Average();
            return avg > 0 ? (int)avg / 10 : 0;
        }

        var groupByPercentileQuery =
            from student in students
            let percentile = GetPercentile(student)
            group new
            {
                student.FirstName,
                student.LastName
            } by percentile into percentGroup
            orderby percentGroup.Key
            select percentGroup;

        foreach (var studentGroup in groupByPercentileQuery)
        {
            Console.WriteLine($"Key: {studentGroup.Key * 10}");
            foreach (var item in studentGroup)
            {
                Console.WriteLine($"\t{item.LastName}, {item.FirstName}");
            }
        }
    }

    private static void UseJoin()
    {
        var teachers = new List<Teacher>();
        var students = new List<Student>();
        var departments = new List<Department>();

        var studentsInDepartments = from student in students
            join department in departments on student.DepartmentID equals department.ID
            select new
            {
                Name = $"{student.FirstName} {student.LastName}",
                DepartmentName = department.Name
            };

        foreach (var item in studentsInDepartments)
        {
            Console.WriteLine($"{item.Name} - {item.DepartmentName}");
        }

        // Join into group
        //2 queries are similar

        IEnumerable<IEnumerable<Student>> studentGroups1 = from department in departments
            join student in students on department.ID equals student.DepartmentID into studentGroup
            select studentGroup;

        foreach (IEnumerable<Student> studentGroup in studentGroups1)
        {
            Console.WriteLine("Group");
            foreach (Student student in studentGroup)
            {
                Console.WriteLine($"  - {student.FirstName}, {student.LastName}");
            }
        }

        // Join department and student based on DepartmentId and grouping result
        IEnumerable<IEnumerable<Student>> studentGroups = departments.GroupJoin(students,
            department => department.ID, student => student.DepartmentID,
            (department, studentGroup) => studentGroup);

        foreach (IEnumerable<Student> studentGroup in studentGroups)
        {
            Console.WriteLine("Group");
            foreach (Student student in studentGroup)
            {
                Console.WriteLine($"  - {student.FirstName}, {student.LastName}");
            }
        }


        // multiple key joins
        IEnumerable<string> query = from teacher in teachers
            join student in students on new
            {
                FirstName = teacher.First,
                LastName = teacher.Last
            } equals new { student.FirstName, student.LastName }
            select teacher.First + " " + teacher.Last;
        string result = "The following people are both teachers and students:\r\n";
        foreach (string name in query)
        {
            result += $"{name}\r\n";
        }

        Console.Write(result);

        IEnumerable<string> queryMethod = teachers
            .Join(students,
                teacher => new { FirstName = teacher.First, LastName = teacher.Last },
                student => new { student.FirstName, student.LastName },
                (teacher, student) => $"{teacher.First} {teacher.Last}"
            );

        Console.WriteLine("The following people are both teachers and students:");
        foreach (string name in queryMethod)
        {
            Console.WriteLine(name);
        }

        // multiple collection joins
        // The first join matches Department.ID and Student.DepartmentID from the list of students and
// departments, based on a common ID. The second join matches teachers who lead departments
// with the students studying in that department.
        var multipleTableQuery = from student in students
            join department in departments on student.DepartmentID equals department.ID
            join teacher in teachers on department.TeacherID equals teacher.ID
            select new
            {
                StudentName = $"{student.FirstName} {student.LastName}",
                DepartmentName = department.Name,
                TeacherName = $"{teacher.First} {teacher.Last}"
            };

        foreach (var obj in multipleTableQuery)
        {
            Console.WriteLine($"""The student "{obj.StudentName}" studies in the department run by "{obj.TeacherName}".""");
        }


        var multipleTableQueryMethod = students
            .Join(departments, 
                    student => student.DepartmentID, 
                    department => department.ID,
                    (student, department) => new { student, department }
                  )
            .Join(teachers, 
                // First collection key selection
                 commonDepartment => commonDepartment.department.TeacherID,
                // Second collection key selection
                 teacher => teacher.ID,
                // Value projection
                (commonDepartment, teacher) => new
                {
                    StudentName = $"{commonDepartment.student.FirstName} {commonDepartment.student.LastName}",
                    DepartmentName = commonDepartment.department.Name,
                    TeacherName = $"{teacher.First} {teacher.Last}"
                });

        foreach (var obj in multipleTableQueryMethod)
        {
            Console.WriteLine($"""The student "{obj.StudentName}" studies in the department run by "{obj.TeacherName}".""");
        }
    }

    private static void ChunkData()
    {
        int chunkNumber = 1;
        foreach (int[] chunk in Enumerable.Range(0, 8).Chunk(3))
        {
            Console.WriteLine($"Chunk {chunkNumber++}:");
            foreach (int item in chunk)
            {
                Console.WriteLine($"    {item}");
            }

            Console.WriteLine();
        }
    }

    private static void SecondarySort()
    {
        //// Then by is the secondary sorting
        //IEnumerable<(string, string)> query = teachers
        //    .OrderBy(teacher => teacher.City)
        //    .ThenBy(teacher => teacher.Last)
        //    .Select(teacher => (teacher.Last, teacher.City));

        //foreach ((string last, string city) in query)
        //{
        //    Console.WriteLine($"City: {city}, Last Name: {last}");
        //}

        //// The above query is equivalent to 
        //IEnumerable<(string, string)> query = from teacher in teachers
        //    orderby teacher.City, teacher.Last
        //    select (teacher.Last, teacher.City);

        //foreach ((string last, string city) in query)
        //{
        //    Console.WriteLine($"City: {city}, Last Name: {last}");
        //}
    }

    private static void UsingZip()
    {
        //The resulting sequence from a zip operation is never longer in length than the shortest sequence
        // An int array with 7 elements.
        IEnumerable<int> numbers = [1, 2, 3, 4, 5, 6, 7, 8, 9];
        // A char array with 6 elements.
        IEnumerable<char> letters = ['A', 'B', 'C', 'D', 'E', 'F'];

        // A string array with 8 elements.
        IEnumerable<string> emoji = ["🤓", "🔥", "🎉", "👀", "⭐", "💜", "✔", "💯"];

        foreach (var (number, letter) in numbers.Zip(letters))
        {
            Console.WriteLine($"{number} zipped with {letter}");
        }

        foreach ((int number, char letter, string em) in numbers.Zip(letters, emoji))
        {
            Console.WriteLine(
                $"Number: {number} is zipped with letter: '{letter}' and emoji: {em}");
        }
    }
}