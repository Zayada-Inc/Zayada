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
  photos: {
    url: '';
  }[];
  token?: string;
}

export interface IGetAllUsersResponse {
  data: {
    id: string;
    displayName: string;
    email: string;
    username: string;
    personalTrainer: {
      id: string;
      certifications: string;
      gymName: string;
    };
    photos: {
      id: string;
      url: string;
      isMain: boolean;
    }[];
  }[];
}
