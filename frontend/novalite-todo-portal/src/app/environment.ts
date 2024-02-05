export const environment = {
  production: false,
  msalConfig: {
    auth: {
      clientId: '33e5695b-7e44-4ac2-87fa-6ec31a06a42e',
      authority: 'https://login.microsoftonline.com/common'
    }
  },
  apiConfig: {
    scopes: ['api://33e5695b-7e44-4ac2-87fa-6ec31a06a42e/user.read'],
    uri: 'https://graph.microsoft.com/v1.0/me'
  }
};
