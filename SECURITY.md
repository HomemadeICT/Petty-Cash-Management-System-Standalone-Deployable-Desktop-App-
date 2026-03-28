# Security Policy

## Overview

This is a **portfolio project** created for educational and demonstration purposes. While security best practices have been implemented, this application is **not suitable for production use without thorough security review and hardening**.

## Security Features Implemented

✅ **Implemented:**
- BCrypt password hashing (never storing plaintext passwords)
- Role-based access control (RBAC) with granular permissions
- Audit logging for all user actions
- Input validation on all forms
- Business rule enforcement
- Session management
- SQLite database with file-based encryption available via 3rd-party tools

## Security Considerations

⚠️ **Important Notes:**

1. **Default Credentials**: The default login credentials (`admin` / `admin123`) are hardcoded for demo purposes. **These MUST be changed immediately** before any production use.

2. **Database**: SQLite is used for portability and zero-configuration. For production:
   - Consider migrating to SQL Server or PostgreSQL with proper authentication
   - Use encrypted connections (TLS/SSL)
   - Implement proper database backup strategies

3. **Local Application**: This is a desktop application running locally. It assumes:
   - The host machine is trusted
   - The user's operating system is properly secured
   - No network connectivity is required (by design)

4. **Configuration Files**: Sensitive configuration should not be hardcoded. Use:
   - Environment variables
   - secure configuration providers
   - Azure Key Vault or similar services

## Reporting Security Vulnerabilities

If you discover a security vulnerability in this project, **please do not open a public GitHub issue**. Instead:

1. Email security details to [contact information if provided]
2. Include a description of the vulnerability and potential impact
3. Give the maintainer reasonable time to respond before public disclosure

This project is not actively maintained for production use, so response time may vary.

## Dependencies Security

This project uses the following third-party libraries:
- `System.Data.SQLite` - SQLite database provider
- `BCrypt.Net-Next` - Password hashing
- `ClosedXML` - Excel file generation
- `System.Configuration.ConfigurationManager` - Configuration management

Keep these dependencies updated for security patches. Use:
```bash
dotnet list package --outdated
```

## Recommendations for Production

Before deploying to a production environment:

1. ✅ Change all default credentials
2. ✅ Review and harden database access controls
3. ✅ Implement encryption for data at rest
4. ✅ Use HTTPS/TLS for any network communication
5. ✅ Implement comprehensive logging and monitoring
6. ✅ Conduct security audit and penetration testing
7. ✅ Follow principle of least privilege for all users
8. ✅ Implement backup and disaster recovery procedures

## License

This project is licensed under the MIT License. See [LICENSE](LICENSE) for details.

---

**Last Updated:** March 2026  
**Project Status:** Portfolio/Educational
