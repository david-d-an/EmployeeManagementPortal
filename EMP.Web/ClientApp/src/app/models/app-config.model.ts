export interface IAppConfig {
  env: {
    name: string;
  };
  appInsights: {
    instrumentationKey: string;
  };
  logging: {
    console: boolean;
    appInsights: boolean;
  };
  aad: {
    requireAuth: boolean;
    tenant: string;
    clientId: string;
  };
  apiServer: {
    employees: string;
    metadata: string;
    rules: string;
  };
  oid: {
    clientRoot: string;
    apiRoot: string;
    stsAuthority: string;
    clientId: string;
    apiId: string;
  };
}
