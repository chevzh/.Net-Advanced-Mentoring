using System;
using System.Collections.Generic;
using System.Text;

namespace Composite.Task2
{
    public class Form : IComponent
    {
        String name;
        List<IComponent> components;

        public Form(String name)
        {
            this.name = name;
            components = new List<IComponent>();
        }

        public void AddComponent(IComponent component)
        {
            components.Add(component);
        }

        public string ConvertToString(int depth = 0)
        {
            StringBuilder content = new StringBuilder();
            string space = new String(' ', depth);
            depth++;

            foreach (IComponent component in components)
            {
                content.Append($"{Environment.NewLine}{component.ConvertToString(depth)}");
            }

            return $@"{space}<form name='{name}'>{content}{Environment.NewLine}{space}</form>";
        }
    }
}