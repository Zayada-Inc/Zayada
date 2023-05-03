import { AddGymModal } from './Variants/AddGymModal';
import { AddPTModal } from './Variants/AddPTModal';
import { AddUserModal } from './Variants/AddUserModal';
import { EditUserModal } from './Variants/EditUserModal';
import { SettingsModal } from './Variants/SettingsModal';

// TO-DO: update this any
export const MODAL_TYPES: any = {
  settings: SettingsModal,
  editUser: EditUserModal,
  addUser: AddUserModal,
  addGym: AddGymModal,
  addPT: AddPTModal,
};
