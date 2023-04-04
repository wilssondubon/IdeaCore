namespace IdeaCoreHateoas.Contracts
{
    public interface IHateoasListWrapperService
    {
        IHateoasListWrapper Wrap(object embed);
    }
}