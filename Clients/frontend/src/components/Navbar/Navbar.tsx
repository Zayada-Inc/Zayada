import i18next from 'i18next';

export const Navbar = () => {
  // temporary
  const handleLanguageChange = (e: any) => {
    i18next.changeLanguage(e.target.value);
  };

  return (
    <div className='bg-gray-300 h-[70px]'>
      <select onClick={handleLanguageChange}>
        <option value='en'>English</option>
        <option value='ro'>Romana</option>
      </select>
      <p>navbar</p>
    </div>
  );
};
