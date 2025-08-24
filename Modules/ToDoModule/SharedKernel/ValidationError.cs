namespace SharedKernel;

public sealed record ValidationError : Error
{
  public ValidationError(Error[] errors)
      : base(
          "Validation.General",
          "One or more validation errors occurred",
          ErrorType.Validation)
  {
    Errors = errors.Select(e => 
      new ErrorValidationDTO(
        e.Code, e.Description, e.Type.ToString()
        )).ToArray();
  }

  public ErrorValidationDTO[] Errors { get; }

  public static ValidationError FromResults(IEnumerable<Result> results) =>
      new(results.Where(r => r.IsFailure).Select(r => r.Error).ToArray());
}

public sealed record ErrorValidationDTO(string Code, string Description, string Type);
