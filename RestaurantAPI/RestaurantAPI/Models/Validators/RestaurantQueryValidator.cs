using FluentValidation;
using RestaurantAPI;

public class RestaurantQueryValidator : AbstractValidator<RestaurantQuery>
{
	private int[] pageSizes = { 5, 10, 15, 30 };
	public RestaurantQueryValidator()
	{
		RuleFor(r=>r.pageSize).GreaterThanOrEqualTo(1);
		RuleFor(r => r.pageSize).Custom((value, context) =>
		{
			if (!pageSizes.Contains(value))
			{
				context.AddFailure("pageSize", $"Page Size must in [{string.Join(',', pageSizes)}]");
			}
		});
	}
}