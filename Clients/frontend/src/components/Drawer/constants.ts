import { Calendar, Home2, Logout, Settings } from 'tabler-icons-react';

export const ICONS_TOP = [
  {
    icon: Home2,
    label: 'Dashboard',
    route: '/dashboard',
  },
  {
    icon: Calendar,
    label: 'Scheduler',
    route: '/scheduler',
  },
];

export const ICONS_BOTTOM = [
  {
    icon: Settings,
    label: 'Settings',
  },
  {
    icon: Logout,
    label: 'Logout',
  },
];
