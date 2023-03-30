export interface IRegisterRequest {
  displayName: string;
  username: string;
  email: string;
  password: string;
}

export interface ILoginRequest {
  email: string;
  password: string;
}

export interface IAuthenticationResponse {
  displayName: string;
  username: string;
  image: string;
  token?: string;
}
