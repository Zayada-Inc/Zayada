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
  id: string;
  displayName: string;
  email: string;
  username: string;
  personalTrainer: {
    id: string;
    certifications: string;
    gymName: string;
    username: string;
    email: string;
    photos: {
      id: string;
      url: string;
      isMain: boolean;
    }[];
  };
  photos: {
    id: string;
    url: string;
    isMain: boolean;
  }[];
  // TO-DO: discuss /w Mihnea if this is still being used
  image?: string;
}
