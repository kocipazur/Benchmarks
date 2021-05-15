using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace Collections
{
    class Program
    {
        static void Main(string[] args)
        {
            _ = BenchmarkRunner.Run<AddOperations>();
            //_ = BenchmarkRunner.Run<DeleteOperations>();
        }
    }
    [MemoryDiagnoser]
    public class AddOperations
    {
        public List<ExampleObject> ListWith1000ExampleObjects;
        public Dictionary<int, ExampleObject> DictionaryWith1000IntKeyExampleObjectsValue;
        private ExampleObject _exampleObject;

        public AddOperations()
        {
            _exampleObject = new ExampleObject
            {
                StringValue = "exampleString",
                IntValue = 1776
            };

            ListWith1000ExampleObjects = Add1000ObjectsToList();
            DictionaryWith1000IntKeyExampleObjectsValue = Add1000ObjectsToDictionaryWithIntKey();
        }

        [Benchmark]
        public List<ExampleObject> Add1000ObjectsToList()
        {
            List<ExampleObject> List = new List<ExampleObject>();
            for (int i = 0; i < 1000; i++)
            {
                List.Add(_exampleObject);
            }

            return List;
        }

        [Benchmark]
        public Dictionary<int, ExampleObject> Add1000ObjectsToDictionaryWithIntKey()
        {
            Dictionary<int, ExampleObject> List = new Dictionary<int, ExampleObject>();
            for (int i = 0; i < 1000; i++)
            {
                List.Add(i, _exampleObject);
            }

            return List;
        }
    }

    public class DeleteOperations
    {
        private readonly AddOperations _addOperations;
        public DeleteOperations()
        {
            _addOperations = new AddOperations();
        }

        [Benchmark]
        public void DeleteFirstElementFromList()
        {
            _addOperations
                .ListWith1000ExampleObjects
                .RemoveAt(0);
        }

        [Benchmark]
        public void DeleteFirstElementFromDictionary()
        {
            _addOperations
                .DictionaryWith1000IntKeyExampleObjectsValue
                .Remove(0);
        }
    }

    public class ExampleObject
    {
        public string StringValue { get; set; }
        public int IntValue { get; set; }
    }
}
