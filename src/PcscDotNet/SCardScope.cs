namespace PcscDotNet
{
    /// <summary>
    /// Scope of the smart card resource manager context.
    /// </summary>
    public enum SCardScope
    {
        /// <summary>
        /// The context is a user context, and any database operations are performed within the domain of the user.
        /// </summary>
        User = 0,
        /// <summary>
        /// The context is that of the current terminal, and any database operations are performed within the domain of that terminal.
        /// (The calling application must have appropriate access permissions for any database actions.)
        /// </summary>
        Terminal = 1,
        /// <summary>
        /// The context is the system context, and any database operations are performed within the domain of the system.
        /// (The calling application must have appropriate access permissions for any database actions.)
        /// </summary>
        System = 2
    }
}