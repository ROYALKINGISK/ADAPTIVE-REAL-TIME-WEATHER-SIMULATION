import pandas as pd
from sklearn.model_selection import train_test_split
from sklearn.preprocessing import StandardScaler, LabelEncoder

# Load the dataset
df = pd.read_csv("/content/drive/MyDrive/My/weather_data_10000_rows.csv")

# Display the first few rows of the dataset
print("Initial data:")
print(df.head())

# Check for missing values
print("\nMissing values in each column:")
print(df.isnull().sum())

# If there are missing values, you can either fill them or drop rows/columns
# For this dataset, we assume there are no missing values as we generated the data

# Encoding the 'Weather' column (categorical data) using LabelEncoder
label_encoder = LabelEncoder()
df['Weather'] = label_encoder.fit_transform(df['Weather'])

# Normalize numerical features ('Temperature (°C)', 'Humidity (%)', and 'Intensity')
scaler = StandardScaler()
df[['Temperature (°C)', 'Humidity (%)', 'Intensity']] = scaler.fit_transform(df[['Temperature (°C)', 'Humidity (%)', 'Intensity']])

# Check the changes
print("\nData after preprocessing:")
print(df.head())

# Split the dataset into features (X) and target (y)
X = df.drop(columns=['Weather'])  # Features: All columns except 'Weather'
y = df['Weather']  # Target: 'Weather' column

# Split the dataset into training and testing sets (80% train, 20% test)
X_train, X_test, y_train, y_test = train_test_split(X, y, test_size=0.2, random_state=42)

print(f"\nTraining data shape: {X_train.shape}")
print(f"Testing data shape: {X_test.shape}")

# Optionally, save the preprocessed dataset to a new CSV
df.to_csv("preprocessed_weather_data.csv", index=False)

print("\nPreprocessing completed and data saved to 'preprocessed_weather_data.csv'.")
