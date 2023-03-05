import { ContentLayout } from 'components/Layout';
import { useTranslation } from 'react-i18next';

export const Dashboard = () => {
  const { t } = useTranslation();

  return (
    <ContentLayout>
      <div className='h-full border'> {t('dashboard.welcomeBack')} </div>
    </ContentLayout>
  );
};
