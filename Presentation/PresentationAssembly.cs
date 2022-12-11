using System.Reflection;

namespace Presentation;

public static class PresentationAssembly
{
    public static Assembly GetAssembly() => Assembly.GetExecutingAssembly();
}