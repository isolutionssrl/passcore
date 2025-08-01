﻿using Novell.Directory.Ldap;
using Unosquare.PassCore.Common;

namespace Zyborg.PassCore.PasswordProvider.LDAP;

/// <summary>
/// Represents the options of this provider.
/// </summary>
/// <seealso cref="Unosquare.PassCore.Common.IAppSettings" />
public class LdapPasswordChangeOptions : IAppSettings
{
    private string[]? ldapHostnames;
    private string? ldapPassword;
    private string? ldapUsername;
    private string? defaultDomain;

    /// <inheritdoc />
    public string[] LdapHostnames
    {
        get => ldapHostnames ?? [];
        set => ldapHostnames = value;
    }

    /// <inheritdoc />
    public string LdapPassword
    {
        get => ldapPassword ?? string.Empty;
        set => ldapPassword = value;
    }

    /// <inheritdoc />
    public string LdapUsername
    {
        get => ldapUsername ?? string.Empty;
        set => ldapUsername = value;
    }

    /// <inheritdoc />
    public string DefaultDomain
    {
        get => defaultDomain ?? string.Empty;
        set => defaultDomain = value;
    }

    /// <inheritdoc />
    public int LdapPort { get; set; } = LdapConnection.DefaultSslPort;

    /// <summary>
    /// Gets or sets a value indicating whether [LDAP uses SSL].
    /// </summary>
    /// <remarks>
    /// Optional, if 'true', then the specified port is using SSL encryption.
    /// By default this should set to 'true' when using port 636.
    /// </remarks>
    /// <value>
    ///   <c>true</c> if [LDAP uses SSL]; otherwise, <c>false</c>.
    /// </value>
    public bool LdapSecureSocketLayer { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether [LDAP start TLS].
    /// </summary>
    /// <remarks>
    /// Optional, if 'true', then the specified port is a non-secured port by default
    /// and requires the use of the "StartTLS" command over LDAP to enable TLS.
    /// </remarks>
    /// <value>
    ///   <c>true</c> if [LDAP start TLS]; otherwise, <c>false</c>.
    /// </value>
    public bool LdapStartTls { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether [LDAP ignore TLS errors].
    /// </summary>
    /// <remarks>
    /// Optional, if 'true', then server certificates will be ignored for expiration
    /// or common name mismatch.  Note this is a SUPERSET of the LdapIgnoreTlsValidation
    /// options, so you don't have to set both.
    /// </remarks>
    /// <value>
    ///   <c>true</c> if [LDAP ignore TLS errors]; otherwise, <c>false</c>.
    /// </value>
    public bool LdapIgnoreTlsErrors { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether [LDAP ignore TLS validation].
    /// </summary>
    /// <remarks>
    /// Optional, if 'true', then server certificates will be accepted regardless
    /// of being signed by a trusted CA or intermediary (e.g. self-signed).
    /// </remarks>
    /// <value>
    ///   <c>true</c> if [LDAP ignore TLS validation]; otherwise, <c>false</c>.
    /// </value>
    public bool LdapIgnoreTlsValidation { get; set; }

    /// <summary>
    /// Gets or sets the LDAP search base.
    /// </summary>
    /// <remarks>
    /// Distinguished Name (DN) of the base OU from which to search for
    /// the target users by their username (SAM Account Name).
    /// </remarks>
    /// <value>
    /// The LDAP search base.
    /// </value>
    public string? LdapSearchBase { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether [hide user not found].
    /// </summary>
    /// <remarks>
    /// When the user cannot be located in the directory, you can
    /// either expose that error, or hide it and treat like an arbitrary
    /// bad credential -- in order to prevent brute force attack to
    /// discover the presence or absence of a username.
    /// </remarks>
    /// <value>
    ///   <c>true</c> if [hide user not found]; otherwise, <c>false</c>.
    /// </value>
    public bool HideUserNotFound { get; set; } = true;

    /// <summary>
    /// Gets or sets a value indicating whether [LDAP change password with delete add].
    /// </summary>
    /// <value>
    ///   <c>true</c> if [LDAP change password with delete add]; otherwise, <c>false</c>.
    /// </value>
    public bool LdapChangePasswordWithDelAdd { get; set; } = true;

    /// <summary>
    /// Gets or sets the LDAP search filter.
    /// </summary>
    /// <value>
    /// The LDAP search filter.
    /// </value>
    public string LdapSearchFilter { get; set; } = "(sAMAccountName={Username})";
}