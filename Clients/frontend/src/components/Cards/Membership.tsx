import { useTranslation } from 'react-i18next';

export const Membership = () => {
  const { t } = useTranslation();

  return (
    // TO-DO: remove margin when layout is implemented
    <div className='h-[250px] w-[430px] bg-primary-color text-white mt-7 rounded-lg'>
      <div className='flex flex-col justify-evenly h-3/5 pl-3'>
        <p className='text-base font-semibold'>
          {t('dashboard.membership.title', { gymName: '18Gym' })}
        </p>
        <p className='text-sm'>{t('dashboard.membership.start')} 24 Feb 2023</p>
        <p className='text-sm'>{t('dashboard.membership.end')} 25 Mar 2023</p>
      </div>
      <div className='flex items-center h-2/5 pl-3 text-sm'>
        <p>{t('dashboard.membership.type')} Premium+</p>
      </div>
    </div>
  );
};
