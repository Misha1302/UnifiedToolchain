namespace CommonExtensions;

public static class TypesExtensions
{
    public static bool IsImplement(this Type type, Type interfaceType) => interfaceType.IsAssignableFrom(type);

    public static bool IsImplement<TInterface>(this Type type) => type.IsImplement(typeof(TInterface));

    public static bool IsChildOrEq(this Type t1, Type t2) => t1.IsSubclassOf(t2) || t1 == t2;

    public static TTo To<TFrom, TTo>(this TFrom value) =>
        typeof(TTo) == typeof(int) && value is long ? (TTo)(object)(int)(long)(object)value
        : typeof(TTo) == typeof(long) && value is int ? (TTo)(object)(long)(int)(object)value
        : (TTo)(object)value!;
}