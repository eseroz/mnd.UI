using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mnd.UI.Modules.Dashboard
{
    public class UlkeEnlemBoylam
    {
        [JsonProperty("country")]
        public string country { get; set; }

        [JsonProperty("latitude")]
        public double latitude { get; set; }

        [JsonProperty("longitude")]
        public double longitude { get; set; }

        [JsonProperty("name")]
        public string name { get; set; }
    }
    public class JsonData
    {

        public static string JsonUlkeLatLong = @"[
  {
    'country': 'AD',
    'latitude': 42.5462456,
    'longitude': 1.601554,
    'name': 'Andorra'
  },
  {
    'country': 'AE',
    'latitude': 23.4240761,
    'longitude': 53.8478165,
    'name': 'United Arab Emirates'
  },
  {
    'country': 'AF',
    'latitude': 33.93911,
    'longitude': 67.70995,
    'name': 'Afghanistan'
  },
  {
    'country': 'AG',
    'latitude': 17.0608158,
    'longitude': -61.79643,
    'name': 'Antigua and Barbuda'
  },
  {
    'country': 'AI',
    'latitude': 18.2205544,
    'longitude': -63.068615,
    'name': 'Anguilla'
  },
  {
    'country': 'AL',
    'latitude': 41.15333,
    'longitude': 20.1683311,
    'name': 'Albania'
  },
  {
    'country': 'AM',
    'latitude': 40.0691,
    'longitude': 45.03819,
    'name': 'Armenia'
  },
  {
    'country': 'AN',
    'latitude': 12.226079,
    'longitude': -69.06009,
    'name': 'Netherlands Antilles'
  },
  {
    'country': 'AO',
    'latitude': -11.202692,
    'longitude': 17.8738861,
    'name': 'Angola'
  },
  {
    'country': 'AQ',
    'latitude': -75.25098,
    'longitude': -0.071389,
    'name': 'Antarctica'
  },
  {
    'country': 'AR',
    'latitude': -38.4160957,
    'longitude': -63.6166725,
    'name': 'Argentina'
  },
  {
    'country': 'AS',
    'latitude': -14.2709723,
    'longitude': -170.132217,
    'name': 'American Samoa'
  },
  {
    'country': 'AT',
    'latitude': 47.51623,
    'longitude': 14.5500717,
    'name': 'Austria'
  },
  {
    'country': 'AU',
    'latitude': -25.2743988,
    'longitude': 133.775131,
    'name': 'Australia'
  },
  {
    'country': 'AW',
    'latitude': 12.52111,
    'longitude': -69.96834,
    'name': 'Aruba'
  },
  {
    'country': 'AZ',
    'latitude': 40.1431046,
    'longitude': 47.5769272,
    'name': 'Azerbaijan'
  },
  {
    'country': 'BA',
    'latitude': 43.9158859,
    'longitude': 17.6790752,
    'name': 'Bosnia and Herzegovina'
  },
  {
    'country': 'BB',
    'latitude': 13.1938868,
    'longitude': -59.5431976,
    'name': 'Barbados'
  },
  {
    'country': 'BD',
    'latitude': 23.6849937,
    'longitude': 90.35633,
    'name': 'Bangladesh'
  },
  {
    'country': 'BE',
    'latitude': 50.5038872,
    'longitude': 4.469936,
    'name': 'Belgium'
  },
  {
    'country': 'BF',
    'latitude': 12.2383327,
    'longitude': -1.561593,
    'name': 'Burkina Faso'
  },
  {
    'country': 'BG',
    'latitude': 42.7338829,
    'longitude': 25.48583,
    'name': 'Bulgaria'
  },
  {
    'country': 'BH',
    'latitude': 25.9304142,
    'longitude': 50.63777,
    'name': 'Bahrain'
  },
  {
    'country': 'BI',
    'latitude': -3.373056,
    'longitude': 29.9188862,
    'name': 'Burundi'
  },
  {
    'country': 'BJ',
    'latitude': 9.30769,
    'longitude': 0,
    'name': 2.315834
  },
  {
    'country': 'BM',
    'latitude': 32.3213844,
    'longitude': -64.75737,
    'name': 'Bermuda'
  },
  {
    'country': 'BN',
    'latitude': 4.535277,
    'longitude': 114.727669,
    'name': 'Brunei'
  },
  {
    'country': 'BO',
    'latitude': -16.2901535,
    'longitude': -63.5886536,
    'name': 'Bolivia'
  },
  {
    'country': 'BR',
    'latitude': -14.2350044,
    'longitude': -51.92528,
    'name': 'Brazil'
  },
  {
    'country': 'BS',
    'latitude': 25.03428,
    'longitude': -77.39628,
    'name': 'Bahamas'
  },
  {
    'country': 'BT',
    'latitude': 27.5141621,
    'longitude': 90.4336,
    'name': 'Bhutan'
  },
  {
    'country': 'BV',
    'latitude': -54.4232,
    'longitude': 3.413194,
    'name': 'Bouvet Island'
  },
  {
    'country': 'BW',
    'latitude': -22.328474,
    'longitude': 24.684866,
    'name': 'Botswana'
  },
  {
    'country': 'BY',
    'latitude': 53.70981,
    'longitude': 27.9533882,
    'name': 'Belarus'
  },
  {
    'country': 'BZ',
    'latitude': 17.1898766,
    'longitude': -88.49765,
    'name': 'Belize'
  },
  {
    'country': 'CA',
    'latitude': 56.1303673,
    'longitude': -106.346771,
    'name': 'Canada'
  },
  {
    'country': 'CC',
    'latitude': -12.1641645,
    'longitude': 96.87096,
    'name': 'Cocos [Keeling] Islands'
  },
  {
    'country': 'CD',
    'latitude': -4.038333,
    'longitude': 21.7586632,
    'name': 'Congo [DRC]'
  },
  {
    'country': 'CF',
    'latitude': 6.611111,
    'longitude': 20.9394436,
    'name': 'Central African Republic'
  },
  {
    'country': 'CG',
    'latitude': -0.228021,
    'longitude': 15.8276587,
    'name': 'Congo [Republic]'
  },
  {
    'country': 'CH',
    'latitude': 46.8181877,
    'longitude': 8.227512,
    'name': 'Switzerland'
  },
  {
    'country': 'CI',
    'latitude': 7.539989,
    'longitude': -5.54708,
    'name': 'Côte d Ivoire'
  },
  {
    'country': 'CK',
    'latitude': -21.2367363,
    'longitude': -159.777664,
    'name': 'Cook Islands'
  },
  {
    'country': 'CL',
    'latitude': -35.675148,
    'longitude': -71.54297,
    'name': 'Chile'
  },
  {
    'country': 'CM',
    'latitude': 7.369722,
    'longitude': 12.354722,
    'name': 'Cameroon'
  },
  {
    'country': 'CN',
    'latitude': 35.86166,
    'longitude': 104.1954,
    'name': 'China'
  },
  {
    'country': 'CO',
    'latitude': 4.570868,
    'longitude': -74.29733,
    'name': 'Colombia'
  },
  {
    'country': 'CR',
    'latitude': 9.748917,
    'longitude': -83.7534256,
    'name': 'Costa Rica'
  },
  {
    'country': 'CU',
    'latitude': 21.5217571,
    'longitude': -77.7811661,
    'name': 'Cuba'
  },
  {
    'country': 'CV',
    'latitude': 16.0020828,
    'longitude': -24.0131969,
    'name': 'Cape Verde'
  },
  {
    'country': 'CX',
    'latitude': -10.447525,
    'longitude': 105.690453,
    'name': 'Christmas Island'
  },
  {
    'country': 'CY',
    'latitude': 35.12641,
    'longitude': 33.42986,
    'name': 'Cyprus'
  },
  {
    'country': 'CZ',
    'latitude': 49.8174934,
    'longitude': 15.4729624,
    'name': 'Czech Republic'
  },
  {
    'country': 'DE',
    'latitude': 51.16569,
    'longitude': 10.4515257,
    'name': 'Germany'
  },
  {
    'country': 'DJ',
    'latitude': 11.8251381,
    'longitude': 42.5902748,
    'name': 'Djibouti'
  },
  {
    'country': 'DK',
    'latitude': 56.26392,
    'longitude': 9.501785,
    'name': 'Denmark'
  },
  {
    'country': 'DM',
    'latitude': 15.414999,
    'longitude': -61.3709755,
    'name': 'Dominica'
  },
  {
    'country': 'DO',
    'latitude': 18.735693,
    'longitude': -70.16265,
    'name': 'Dominican Republic'
  },
  {
    'country': 'DZ',
    'latitude': 28.033886,
    'longitude': 1.659626,
    'name': 'Algeria'
  },
  {
    'country': 'EC',
    'latitude': -1.831239,
    'longitude': -78.1834,
    'name': 'Ecuador'
  },
  {
    'country': 'EE',
    'latitude': 58.5952721,
    'longitude': 25.013607,
    'name': 'Estonia'
  },
  {
    'country': 'EG',
    'latitude': 26.8205528,
    'longitude': 30.8024979,
    'name': 'Egypt'
  },
  {
    'country': 'EH',
    'latitude': 24.2155266,
    'longitude': -12.8858337,
    'name': 'Western Sahara'
  },
  {
    'country': 'ER',
    'latitude': 15.1793842,
    'longitude': 39.7823334,
    'name': 'Eritrea'
  },
  {
    'country': 'ES',
    'latitude': 40.46367,
    'longitude': -3.74922,
    'name': 'Spain'
  },
  {
    'country': 'ET',
    'latitude': 9.145,
    'longitude': 0,
    'name': 40.4896736
  },
  {
    'country': 'FI',
    'latitude': 61.92411,
    'longitude': 25.7481518,
    'name': 'Finland'
  },
  {
    'country': 'FJ',
    'latitude': -16.5781937,
    'longitude': 179.414413,
    'name': 'Fiji'
  },
  {
    'country': 'FK',
    'latitude': -51.7962532,
    'longitude': -59.523613,
    'name': 'Falkland Islands [Islas Malvinas]'
  },
  {
    'country': 'FM',
    'latitude': 7.425554,
    'longitude': 150.550812,
    'name': 'Micronesia'
  },
  {
    'country': 'FO',
    'latitude': 61.8926353,
    'longitude': -6.911806,
    'name': 'Faroe Islands'
  },
  {
    'country': 'FR',
    'latitude': 46.22764,
    'longitude': 2.213749,
    'name': 'France'
  },
  {
    'country': 'GA',
    'latitude': -0.803689,
    'longitude': 11.6094437,
    'name': 'Gabon'
  },
  {
    'country': 'UK',
    'latitude': 55.37805,
    'longitude': -3.435973,
    'name': 'United Kingdom'
  },
  {
    'country': 'GD',
    'latitude': 12.2627764,
    'longitude': -61.60417,
    'name': 'Grenada'
  },
  {
    'country': 'GE',
    'latitude': 42.3154068,
    'longitude': 43.35689,
    'name': 'Georgia'
  },
  {
    'country': 'GF',
    'latitude': 3.933889,
    'longitude': -53.125782,
    'name': 'French Guiana'
  },
  {
    'country': 'GG',
    'latitude': 49.46569,
    'longitude': -2.585278,
    'name': 'Guernsey'
  },
  {
    'country': 'GH',
    'latitude': 7.946527,
    'longitude': -1.023194,
    'name': 'Ghana'
  },
  {
    'country': 'GI',
    'latitude': 36.13774,
    'longitude': -5.345374,
    'name': 'Gibraltar'
  },
  {
    'country': 'GL',
    'latitude': 71.70694,
    'longitude': -42.6043,
    'name': 'Greenland'
  },
  {
    'country': 'GM',
    'latitude': 13.443182,
    'longitude': -15.3101387,
    'name': 'Gambia'
  },
  {
    'country': 'GN',
    'latitude': 9.945587,
    'longitude': -9.696645,
    'name': 'Guinea'
  },
  {
    'country': 'GP',
    'latitude': 16.9959717,
    'longitude': -62.0676422,
    'name': 'Guadeloupe'
  },
  {
    'country': 'GQ',
    'latitude': 1.650801,
    'longitude': 10.2678947,
    'name': 'Equatorial Guinea'
  },
  {
    'country': 'GR',
    'latitude': 39.0742073,
    'longitude': 21.8243122,
    'name': 'Greece'
  },
  {
    'country': 'GS',
    'latitude': -54.42958,
    'longitude': -36.58791,
    'name': 'South Georgia and the South Sandwich Islands'
  },
  {
    'country': 'GT',
    'latitude': 15.7834711,
    'longitude': -90.23076,
    'name': 'Guatemala'
  },
  {
    'country': 'GU',
    'latitude': 13.4443045,
    'longitude': 144.793732,
    'name': 'Guam'
  },
  {
    'country': 'GW',
    'latitude': 11.8037491,
    'longitude': -15.1804132,
    'name': 'Guinea-Bissau'
  },
  {
    'country': 'GY',
    'latitude': 4.860416,
    'longitude': -58.93018,
    'name': 'Guyana'
  },
  {
    'country': 'GZ',
    'latitude': 31.3546753,
    'longitude': 34.3088264,
    'name': 'Gaza Strip'
  },
  {
    'country': 'HK',
    'latitude': 22.3964272,
    'longitude': 114.1095,
    'name': 'Hong Kong'
  },
  {
    'country': 'HM',
    'latitude': -53.08181,
    'longitude': 73.50416,
    'name': 'Heard Island and McDonald Islands'
  },
  {
    'country': 'HN',
    'latitude': 15.1999989,
    'longitude': -86.2419052,
    'name': 'Honduras'
  },

 {
    'country': 'HR',
    'latitude': 45.815399,
    'longitude': 15.966568,
    'name': 'Honduras'
  },
  {
    'country': 'HT',
    'latitude': 18.9711876,
    'longitude': -72.28522,
    'name': 'Haiti'
  },
  {
    'country': 'HU',
    'latitude': 47.1624947,
    'longitude': 19.5033035,
    'name': 'Hungary'
  },
  {
    'country': 'ID',
    'latitude': -0.789275,
    'longitude': 113.921326,
    'name': 'Indonesia'
  },
  {
    'country': 'IE',
    'latitude': 53.41291,
    'longitude': -8.24389,
    'name': 'Ireland'
  },
  {
    'country': 'IL',
    'latitude': 31.046051,
    'longitude': 34.8516121,
    'name': 'Israel'
  },
  {
    'country': 'IM',
    'latitude': 54.2361069,
    'longitude': -4.548056,
    'name': 'Isle of Man'
  },
  {
    'country': 'IN',
    'latitude': 20.5936832,
    'longitude': 78.96288,
    'name': 'India'
  },
  {
    'country': 'IO',
    'latitude': -6.343194,
    'longitude': 71.87652,
    'name': 'British Indian Ocean Territory'
  },
  {
    'country': 'IQ',
    'latitude': 33.22319,
    'longitude': 43.67929,
    'name': 'Iraq'
  },
  {
    'country': 'IR',
    'latitude': 32.42791,
    'longitude': 53.6880455,
    'name': 'Iran'
  },
  {
    'country': 'IS',
    'latitude': 64.96305,
    'longitude': -19.0208359,
    'name': 'Iceland'
  },
  {
    'country': 'IT',
    'latitude': 41.87194,
    'longitude': 12.56738,
    'name': 'Italy'
  },
  {
    'country': 'JE',
    'latitude': 49.21444,
    'longitude': -2.13125,
    'name': 'Jersey'
  },
  {
    'country': 'JM',
    'latitude': 18.109581,
    'longitude': -77.29751,
    'name': 'Jamaica'
  },
  {
    'country': 'JO',
    'latitude': 30.5851631,
    'longitude': 36.2384148,
    'name': 'Jordan'
  },
  {
    'country': 'JP',
    'latitude': 36.2048225,
    'longitude': 138.25293,
    'name': 'Japan'
  },
  {
    'country': 'KE',
    'latitude': -0.023559,
    'longitude': 37.9061928,
    'name': 'Kenya'
  },
  {
    'country': 'KG',
    'latitude': 41.20438,
    'longitude': 74.7661,
    'name': 'Kyrgyzstan'
  },
  {
    'country': 'KH',
    'latitude': 12.5656786,
    'longitude': 104.990967,
    'name': 'Cambodia'
  },
  {
    'country': 'KI',
    'latitude': -3.370417,
    'longitude': -168.734039,
    'name': 'Kiribati'
  },
  {
    'country': 'KM',
    'latitude': -11.875001,
    'longitude': 43.87222,
    'name': 'Comoros'
  },
  {
    'country': 'KN',
    'latitude': 17.3578224,
    'longitude': -62.7829971,
    'name': 'Saint Kitts and Nevis'
  },
  {
    'country': 'KP',
    'latitude': 40.33985,
    'longitude': 127.510094,
    'name': 'North Korea'
  },
  {
    'country': 'KR',
    'latitude': 35.9077568,
    'longitude': 127.766922,
    'name': 'South Korea'
  },
  {
    'country': 'KW',
    'latitude': 29.31166,
    'longitude': 47.4817657,
    'name': 'Kuwait'
  },
  {
    'country': 'KY',
    'latitude': 19.51347,
    'longitude': -80.5669556,
    'name': 'Cayman Islands'
  },
  {
    'country': 'KZ',
    'latitude': 48.0195732,
    'longitude': 66.92368,
    'name': 'Kazakhstan'
  },
  {
    'country': 'LA',
    'latitude': 19.85627,
    'longitude': 102.4955,
    'name': 'Laos'
  },
  {
    'country': 'LB',
    'latitude': 33.85472,
    'longitude': 35.8622856,
    'name': 'Lebanon'
  },
  {
    'country': 'LC',
    'latitude': 13.9094439,
    'longitude': -60.9788933,
    'name': 'Saint Lucia'
  },
  {
    'country': 'LI',
    'latitude': 47.166,
    'longitude': 9.555373,
    'name': 'Liechtenstein'
  },
  {
    'country': 'LK',
    'latitude': 7.873054,
    'longitude': 80.7718,
    'name': 'Sri Lanka'
  },
  {
    'country': 'LR',
    'latitude': 6.428055,
    'longitude': -9.429499,
    'name': 'Liberia'
  },
  {
    'country': 'LS',
    'latitude': -29.6099873,
    'longitude': 28.2336082,
    'name': 'Lesotho'
  },
  {
    'country': 'LT',
    'latitude': 55.1694374,
    'longitude': 23.8812752,
    'name': 'Lithuania'
  },
  {
    'country': 'LU',
    'latitude': 49.8152733,
    'longitude': 6.129583,
    'name': 'Luxembourg'
  },
  {
    'country': 'LV',
    'latitude': 56.8796349,
    'longitude': 24.60319,
    'name': 'Latvia'
  },
  {
    'country': 'LY',
    'latitude': 26.3351,
    'longitude': 0,
    'name': 17.22833
  },
  {
    'country': 'MA',
    'latitude': 31.7917023,
    'longitude': -7.09262,
    'name': 'Morocco'
  },
  {
    'country': 'MC',
    'latitude': 43.7502975,
    'longitude': 7.412841,
    'name': 'Monaco'
  },
  {
    'country': 'MD',
    'latitude': 47.4116325,
    'longitude': 28.3698845,
    'name': 'Moldova'
  },
  {
    'country': 'ME',
    'latitude': 42.70868,
    'longitude': 19.37439,
    'name': 'Montenegro'
  },
  {
    'country': 'MG',
    'latitude': -18.7669468,
    'longitude': 46.8691063,
    'name': 'Madagascar'
  },
  {
    'country': 'MH',
    'latitude': 7.131474,
    'longitude': 171.184479,
    'name': 'Marshall Islands'
  },
  {
    'country': 'MK',
    'latitude': 41.6086349,
    'longitude': 21.7452755,
    'name': 'Macedonia [FYROM]'
  },
  {
    'country': 'ML',
    'latitude': 17.5706921,
    'longitude': -3.996166,
    'name': 'Mali'
  },
  {
    'country': 'MM',
    'latitude': 21.9139652,
    'longitude': 95.95622,
    'name': 'Myanmar [Burma]'
  },
  {
    'country': 'MN',
    'latitude': 46.8624954,
    'longitude': 103.846657,
    'name': 'Mongolia'
  },
  {
    'country': 'MO',
    'latitude': 22.1987457,
    'longitude': 113.543877,
    'name': 'Macau'
  },
  {
    'country': 'MP',
    'latitude': 17.33083,
    'longitude': 145.384689,
    'name': 'Northern Mariana Islands'
  },
  {
    'country': 'MQ',
    'latitude': 14.6415281,
    'longitude': -61.0241737,
    'name': 'Martinique'
  },
  {
    'country': 'MR',
    'latitude': 21.00789,
    'longitude': -10.940835,
    'name': 'Mauritania'
  },
  {
    'country': 'MS',
    'latitude': 16.7424984,
    'longitude': -62.1873665,
    'name': 'Montserrat'
  },
  {
    'country': 'MT',
    'latitude': 35.9374962,
    'longitude': 14.3754158,
    'name': 'Malta'
  },
  {
    'country': 'MU',
    'latitude': -20.3484039,
    'longitude': 57.55215,
    'name': 'Mauritius'
  },
  {
    'country': 'MV',
    'latitude': 3.202778,
    'longitude': 73.22068,
    'name': 'Maldives'
  },
  {
    'country': 'MW',
    'latitude': -13.2543077,
    'longitude': 34.3015251,
    'name': 'Malawi'
  },
  {
    'country': 'MX',
    'latitude': 23.6345,
    'longitude': -102.552788,
    'name': 'Mexico'
  },
  {
    'country': 'MY',
    'latitude': 4.210484,
    'longitude': 101.975769,
    'name': 'Malaysia'
  },
  {
    'country': 'MZ',
    'latitude': -18.6656952,
    'longitude': 35.5295639,
    'name': 'Mozambique'
  },
  {
    'country': 'NA',
    'latitude': -22.95764,
    'longitude': 18.49041,
    'name': 'Namibia'
  },
  {
    'country': 'NC',
    'latitude': -20.9043045,
    'longitude': 165.618042,
    'name': 'New Caledonia'
  },
  {
    'country': 'NE',
    'latitude': 17.6077881,
    'longitude': 8.081666,
    'name': 'Niger'
  },
  {
    'country': 'NF',
    'latitude': -29.0408344,
    'longitude': 167.954712,
    'name': 'Norfolk Island'
  },
  {
    'country': 'NG',
    'latitude': 9.081999,
    'longitude': 8.675277,
    'name': 'Nigeria'
  },
  {
    'country': 'NI',
    'latitude': 12.8654156,
    'longitude': -85.20723,
    'name': 'Nicaragua'
  },
  {
    'country': 'NL',
    'latitude': 52.1326332,
    'longitude': 5.291266,
    'name': 'Netherlands'
  },
  {
    'country': 'NO',
    'latitude': 60.472023,
    'longitude': 8.468946,
    'name': 'Norway'
  },
  {
    'country': 'NP',
    'latitude': 28.3948574,
    'longitude': 84.12401,
    'name': 'Nepal'
  },
  {
    'country': 'NR',
    'latitude': -0.522778,
    'longitude': 166.9315,
    'name': 'Nauru'
  },
  {
    'country': 'NU',
    'latitude': -19.0544453,
    'longitude': -169.867233,
    'name': 'Niue'
  },
  {
    'country': 'NZ',
    'latitude': -40.90056,
    'longitude': 174.885971,
    'name': 'New Zealand'
  },
  {
    'country': 'OM',
    'latitude': 21.5125828,
    'longitude': 55.9232559,
    'name': 'Oman'
  },
  {
    'country': 'PA',
    'latitude': 8.537981,
    'longitude': -80.78213,
    'name': 'Panama'
  },
  {
    'country': 'PE',
    'latitude': -9.189967,
    'longitude': -75.01515,
    'name': 'Peru'
  },
  {
    'country': 'PF',
    'latitude': -17.6797428,
    'longitude': -149.406845,
    'name': 'French Polynesia'
  },
  {
    'country': 'PG',
    'latitude': -6.314993,
    'longitude': 143.955551,
    'name': 'Papua New Guinea'
  },
  {
    'country': 'PH',
    'latitude': 12.8797207,
    'longitude': 121.774017,
    'name': 'Philippines'
  },
  {
    'country': 'PK',
    'latitude': 30.37532,
    'longitude': 69.3451157,
    'name': 'Pakistan'
  },
  {
    'country': 'PL',
    'latitude': 51.9194374,
    'longitude': 19.1451359,
    'name': 'Poland'
  },
  {
    'country': 'PM',
    'latitude': 46.9419365,
    'longitude': -56.27111,
    'name': 'Saint Pierre and Miquelon'
  },
  {
    'country': 'PN',
    'latitude': -24.7036152,
    'longitude': -127.439308,
    'name': 'Pitcairn Islands'
  },
  {
    'country': 'PR',
    'latitude': 18.2208328,
    'longitude': -66.59015,
    'name': 'Puerto Rico'
  },
  {
    'country': 'PS',
    'latitude': 31.9521618,
    'longitude': 35.2331543,
    'name': 'Palestinian Territories'
  },
  {
    'country': 'PT',
    'latitude': 39.39987,
    'longitude': -8.224454,
    'name': 'Portugal'
  },
  {
    'country': 'PW',
    'latitude': 7.51498,
    'longitude': 0,
    'name': 134.58252
  },
  {
    'country': 'PY',
    'latitude': -23.442503,
    'longitude': -58.4438324,
    'name': 'Paraguay'
  },
  {
    'country': 'QA',
    'latitude': 25.354826,
    'longitude': 51.1838837,
    'name': 'Qatar'
  },
  {
    'country': 'RE',
    'latitude': -21.11514,
    'longitude': 55.5363846,
    'name': 'Réunion'
  },
  {
    'country': 'RO',
    'latitude': 45.94316,
    'longitude': 24.96676,
    'name': 'Romania'
  },
  {
    'country': 'RS',
    'latitude': 44.01652,
    'longitude': 21.00586,
    'name': 'Serbia'
  },
  {
    'country': 'RU',
    'latitude': 61.52401,
    'longitude': 105.318756,
    'name': 'Russia'
  },
  {
    'country': 'RW',
    'latitude': -1.940278,
    'longitude': 29.873888,
    'name': 'Rwanda'
  },
  {
    'country': 'SA',
    'latitude': 23.8859425,
    'longitude': 45.0791626,
    'name': 'Saudi Arabia'
  },
  {
    'country': 'SB',
    'latitude': -9.64571,
    'longitude': 160.156189,
    'name': 'Solomon Islands'
  },
  {
    'country': 'SC',
    'latitude': -4.679574,
    'longitude': 55.4919777,
    'name': 'Seychelles'
  },
  {
    'country': 'SD',
    'latitude': 12.8628073,
    'longitude': 30.2176361,
    'name': 'Sudan'
  },
  {
    'country': 'SE',
    'latitude': 60.1281624,
    'longitude': 18.6435013,
    'name': 'Sweden'
  },
  {
    'country': 'SG',
    'latitude': 1.352083,
    'longitude': 103.819839,
    'name': 'Singapore'
  },
  {
    'country': 'SH',
    'latitude': -24.1434746,
    'longitude': -10.0306959,
    'name': 'Saint Helena'
  },
  {
    'country': 'SI',
    'latitude': 46.15124,
    'longitude': 14.9954634,
    'name': 'Slovenia'
  },
  {
    'country': 'SJ',
    'latitude': 77.5536041,
    'longitude': 23.6702728,
    'name': 'Svalbard and Jan Mayen'
  },
  {
    'country': 'SK',
    'latitude': 48.6690254,
    'longitude': 19.6990242,
    'name': 'Slovakia'
  },
  {
    'country': 'SL',
    'latitude': 8.460555,
    'longitude': -11.7798891,
    'name': 'Sierra Leone'
  },
  {
    'country': 'SM',
    'latitude': 43.94236,
    'longitude': 12.457777,
    'name': 'San Marino'
  },
  {
    'country': 'SN',
    'latitude': 14.4974012,
    'longitude': -14.4523621,
    'name': 'Senegal'
  },
  {
    'country': 'SO',
    'latitude': 5.152149,
    'longitude': 46.1996155,
    'name': 'Somalia'
  },

{
    'country': 'SR',
    'latitude': 3.919305,
    'longitude': -56.027783,
    'name': 'Suriname'
  },
 
  {
    'country': 'ST',
    'latitude': 0.18636,
    'longitude': 0,
    'name': 6.613081
  },
  {
    'country': 'SV',
    'latitude': 13.7941847,
    'longitude': -88.89653,
    'name': 'El Salvador'
  },
  {
    'country': 'SY',
    'latitude': 34.8020744,
    'longitude': 38.9968147,
    'name': 'Syria'
  },
  {
    'country': 'SZ',
    'latitude': -26.5225029,
    'longitude': 31.4658661,
    'name': 'Swaziland'
  },
  {
    'country': 'TC',
    'latitude': 21.694025,
    'longitude': -71.79793,
    'name': 'Turks and Caicos Islands'
  },
  {
    'country': 'TD',
    'latitude': 15.4541664,
    'longitude': 18.7322063,
    'name': 'Chad'
  },
  {
    'country': 'TF',
    'latitude': -49.280365,
    'longitude': 69.34856,
    'name': 'French Southern Territories'
  },
  {
    'country': 'TG',
    'latitude': 8.619543,
    'longitude': 0.824782,
    'name': 'Togo'
  },
  {
    'country': 'TH',
    'latitude': 15.8700323,
    'longitude': 100.992538,
    'name': 'Thailand'
  },
  {
    'country': 'TJ',
    'latitude': 38.8610344,
    'longitude': 71.27609,
    'name': 'Tajikistan'
  },
  {
    'country': 'TK',
    'latitude': -8.967363,
    'longitude': -171.855881,
    'name': 'Tokelau'
  },
  {
    'country': 'TL',
    'latitude': -8.874217,
    'longitude': 125.727539,
    'name': 'Timor-Leste'
  },
  {
    'country': 'TM',
    'latitude': 38.96972,
    'longitude': 59.55628,
    'name': 'Turkmenistan'
  },
  {
    'country': 'TN',
    'latitude': 33.8869171,
    'longitude': 9.537499,
    'name': 'Tunisia'
  },
  {
    'country': 'TO',
    'latitude': -21.1789856,
    'longitude': -175.198242,
    'name': 'Tonga'
  },
  {
    'country': 'TR',
    'latitude': 38.9637451,
    'longitude': 35.24332,
    'name': 'Turkey'
  },
  {
    'country': 'TT',
    'latitude': 10.691803,
    'longitude': -61.2225037,
    'name': 'Trinidad and Tobago'
  },
  {
    'country': 'TV',
    'latitude': -7.109535,
    'longitude': 177.649323,
    'name': 'Tuvalu'
  },
  {
    'country': 'TW',
    'latitude': 23.69781,
    'longitude': 120.960518,
    'name': 'Taiwan'
  },
  {
    'country': 'TZ',
    'latitude': -6.369028,
    'longitude': 34.88882,
    'name': 'Tanzania'
  },
  {
    'country': 'UA',
    'latitude': 48.3794327,
    'longitude': 31.16558,
    'name': 'Ukraine'
  },
  {
    'country': 'UG',
    'latitude': 1.373333,
    'longitude': 32.2902756,
    'name': 'Uganda'
  },
  {
    'country': 'US',
    'latitude': 37.09024,
    'longitude': -95.71289,
    'name': 'United States'
  },
  {
    'country': 'UY',
    'latitude': -32.5227776,
    'longitude': -55.7658348,
    'name': 'Uruguay'
  },
  {
    'country': 'UZ',
    'latitude': 41.37749,
    'longitude': 64.58526,
    'name': 'Uzbekistan'
  },
  {
    'country': 'VA',
    'latitude': 41.902916,
    'longitude': 12.4533892,
    'name': 'Vatican City'
  },
  {
    'country': 'VC',
    'latitude': 12.9843054,
    'longitude': -61.2872276,
    'name': 'Saint Vincent and the Grenadines'
  },
  {
    'country': 'VE',
    'latitude': 6.42375,
    'longitude': 0,
    'name': -66.58973
  },
  {
    'country': 'VG',
    'latitude': 18.4206944,
    'longitude': -64.63997,
    'name': 'British Virgin Islands'
  },
  {
    'country': 'VI',
    'latitude': 18.3357658,
    'longitude': -64.89633,
    'name': 'U.S. Virgin Islands'
  },
  {
    'country': 'VN',
    'latitude': 14.0583239,
    'longitude': 108.2772,
    'name': 'Vietnam'
  },
  {
    'country': 'VU',
    'latitude': -15.3767061,
    'longitude': 166.959152,
    'name': 'Vanuatu'
  },
  {
    'country': 'WF',
    'latitude': -13.7687521,
    'longitude': -177.1561,
    'name': 'Wallis and Futuna'
  },
  {
    'country': 'WS',
    'latitude': -13.7590294,
    'longitude': -172.10463,
    'name': 'Samoa'
  },
  {
    'country': 'XK',
    'latitude': 42.6026344,
    'longitude': 20.902977,
    'name': 'Kosovo'
  },
  {
    'country': 'YE',
    'latitude': 15.5527267,
    'longitude': 48.5163879,
    'name': 'Yemen'
  },
  {
    'country': 'YT',
    'latitude': -12.8275,
    'longitude': 45.1662445,
    'name': 'Mayotte'
  },
  {
    'country': 'ZA',
    'latitude': -30.5594826,
    'longitude': 22.9375057,
    'name': 'South Africa'
  },
  {
    'country': 'ZM',
    'latitude': -13.1338968,
    'longitude': 27.8493328,
    'name': 'Zambia'
  },
  {
    'country': 'ZW',
    'latitude': -19.0154381,
    'longitude': 29.1548576,
    'name': 'Zimbabwe'
  }
]";

    }
}
