using System.Reflection;

namespace WebApi;

public class PresentationAssemblyReference
{
    internal static readonly Assembly assembly = typeof(PresentationAssemblyReference).Assembly;
}