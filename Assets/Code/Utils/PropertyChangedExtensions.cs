using System;
using System.ComponentModel;
using System.Linq.Expressions;

public static class PropertyChangedExtensions
{
    public static IDisposable WhenPropertyChanged<TSource, TProperty>(this TSource source, Expression<Func<TSource, TProperty>> expr, Action<TProperty> action)
        where TSource: INotifyPropertyChanged
    {
        var member = expr.Body as MemberExpression;
        if (member != null)
        {
            var propertyName = member.Member.Name;
            
            PropertyChangedEventHandler onPropertyChanged = (s, e) =>
            {
                if (e.PropertyName == propertyName)
                {
                    action(expr.Compile().Invoke((TSource)s));
                }
            }; 
            
            var propertyChanged = source as INotifyPropertyChanged;
            
            propertyChanged.PropertyChanged += onPropertyChanged;
            return new ActionDisposable(() => {
                propertyChanged.PropertyChanged -= onPropertyChanged;
            });
        }
        
        return new EmptyDisposable();
    }
}