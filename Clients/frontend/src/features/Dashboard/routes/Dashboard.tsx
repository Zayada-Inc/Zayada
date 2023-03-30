import { useTranslation } from 'react-i18next';

import { ContentLayout } from 'components/Layout';
import { Membership } from 'components/Cards';
import { Accordion } from 'components/Headless';
import { Trainers } from 'features/Dashboard/components/Trainers';

export const Dashboard = () => {
  const { t } = useTranslation();

  return (
    <ContentLayout>
      <div className='h-full border'>
        <p className='text-center mt-4 text-2xl font-semibold'>
          {t('dashboard.welcomeBack')}
          <span className='bg-gradient-to-r from-secondary-color to-primary-color bg-clip-text text-transparent'>
            Username!
          </span>
        </p>
        <Trainers numOfCards={3} />
        <Membership />
        <div className='w-[500px]'>
          <Accordion />
        </div>
      </div>
    </ContentLayout>
  );
};
