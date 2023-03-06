import { useTranslation } from 'react-i18next';

import { ContentLayout } from 'components/Layout';
import { Trainers } from './Trainers';

export const Dashboard = () => {
  const { t } = useTranslation();

  return (
    <ContentLayout>
      <div className='h-full border'>
        {t('dashboard.welcomeBack')}
        <Trainers numOfCards={3} />
      </div>
    </ContentLayout>
  );
};
