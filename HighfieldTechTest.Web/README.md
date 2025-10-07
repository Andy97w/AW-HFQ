# Highfield tech test frontend

This is a react frontend for the Highfield tech test.

It makes an API call which returns some user information. It then displays this information about users in separate tables.

Supports dark and light mode.

## Configuration

The application uses environment variables for configuration:

- `VITE_API_BASE_URL`: Base URL for the API (default: https://localhost:7299)

To run locally:
1. Copy `.env` to `.env.local` if you need custom settings
2. Update `VITE_API_BASE_URL` in your environment file as needed
3. Run `npm run dev`

## Environment Files

- `.env` - Default development settings
- `.env.production` - Production settings
- `.env.local` - Local overrides (gitignored)
