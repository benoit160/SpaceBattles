using System.Reflection;
using Microsoft.AspNetCore.Components;
using SpaceBattles.UI.Pages;

namespace SpaceBattles.UI.Tests.Common;

public static class BlazorAssembly
{
    public static IEnumerable<Type> GetBlazorTypes(Assembly assembly)
    {
        Type[] types = assembly.GetTypes();

        IEnumerable<Type> iComponents = types
            .Where(t => t.GetInterfaces()
                .Contains(typeof(IComponent)));

        return iComponents;
    }

    public static IEnumerable<Type> GetBlazorPages(Assembly assembly)
    {
        Type[] types = assembly.GetTypes();

        IEnumerable<Type> iComponents = types
            .Where(t => t.GetInterfaces()
                .Contains(typeof(IComponent)));

        IEnumerable<Type> pages = iComponents
            .Where(c => c.GetCustomAttributes(inherit: true)
                .OfType<RouteAttribute>()
                .Count() != 0);

        return pages;
    }

    public static IEnumerable<Type> GetBlazorComponents(Assembly assembly)
    {
        Type[] types = assembly.GetTypes();

        IEnumerable<Type> iComponents = types
            .Where(t => t.GetInterfaces()
                .Contains(typeof(IComponent)));

        IEnumerable<Type> pages = iComponents
            .Where(c => !c.GetCustomAttributes(inherit: true)
                .OfType<RouteAttribute>().Any());

        return pages;
    }
}