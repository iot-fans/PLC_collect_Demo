
namespace Fins.Message
{
    /// <summary>
    /// Methods specific to a Fins request message.
    /// </summary>
    public interface IFinsRequest : IFinsMessage
    {
        /// <summary>
        /// Validate the specified response against the current request.
        /// </summary>
        void ValidateResponse(IFinsMessage response);
    }
}
